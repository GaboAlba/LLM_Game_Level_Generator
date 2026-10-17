# Best single algorithm for grid-based map generation (any genre)

## Recommendation

**Wave Function Collapse (WFC)** — a constraint-propagation algorithm that fills a grid
one cell at a time by repeatedly collapsing the lowest-entropy cell to a single tile and
propagating the resulting adjacency constraints to its neighbors, backtracking on
contradiction.

WFC is the right answer to "one algorithm, any genre, any grid" because the algorithm
itself carries **zero domain knowledge**. It only understands "which tile IDs are allowed
next to which other tile IDs, in which direction" — a table you supply. Whether that table
encodes a platformer (`smbtile`), a dungeon crawler (`zelda`), a cave (`mdungeons`), or a
puzzle grid (`sokoban`) is entirely external to the algorithm. A single implementation of
the collapse/propagate loop, running over this repo's own
`ArraySpace((h, w), IntegerSpace(n))` content shape, works unmodified across every
dense-grid problem in `pcg_benchmark`.

## How WFC works

1. **State.** Every cell starts with a *domain* — the full set of tile IDs it could still
   become (e.g. all `n` tiles from `IntegerSpace(n)`).
2. **Observe (collapse).** Pick the cell with the fewest remaining possibilities (lowest
   Shannon entropy over its domain, weighted by how frequently each tile appears in the
   source rules). Randomly collapse it to one tile, weighted by those frequencies.
3. **Propagate.** For each neighbor of the collapsed cell, remove any tile from the
   neighbor's domain that has no compatible tile in the collapsed cell's new (singleton)
   domain, per the adjacency table. This can cascade outward (BFS/queue over affected
   cells) since shrinking one neighbor's domain can shrink the *next* neighbor's domain.
4. **Repeat** steps 2-3 until every cell is collapsed (success) or some cell's domain
   becomes empty (**contradiction**) — undo the last few collapses (or restart with a new
   RNG draw) and try again.
5. Two ways to build the adjacency table:
   - **Simple tiled model** — you hand-author which tiles may sit next to which (this maps
     directly onto the tile legends already documented in every `pcg_benchmark`
     `probs/*/README.md`, e.g. zelda's 6 tiles: wall/empty/player/key/door/enemy).
   - **Overlapping model** — you feed WFC one or more example grids and it *extracts* the
     adjacency/frequency rules automatically from NxN patterns found in the examples,
     rather than requiring you to write rules by hand.

Contradiction handling (step 4) is the main practical complexity: naive WFC without
backtracking can dead-end on large or heavily-constrained grids and simply has to restart;
production implementations add local backtracking (undo the last *k* collapses) to avoid
throwing away all progress on a contradiction late in a large map.

## Why WFC over the alternatives, specifically for this repo

`pcg_benchmark/pcg_benchmark/probs/problem.py` defines `Problem.info/quality/diversity/
controlability/render` against an opaque `content: any`, and nearly every concrete problem
represents that content as `ArraySpace((h, w), IntegerSpace(n))` — a dense small-integer 2D
grid: `binary` (2 tiles), `sokoban` (5), `zelda` (6), `ddave`/`loderunnertile` (7),
`mdungeons` (8), `smbtile` (10). (A minority — `isaac`, `building`, `talakat` — are
dict/array-hybrid, non-dense-grid content; see the Limitations section below.)

- **vs. Cellular Automata.** CA (the classic "cave generator": a tile flips based on how
  many of its neighbors are walls) is fast and simple but the *rule itself* is tuned to one
  visual style. The PCG survey literature is explicit about this: constructive CA
  algorithms "lack control and cannot be easily adapted for generating other maps"
  (Procedural Content Generation in Games survey, AIIDE 2024). It doesn't generalize across
  genres the way a swappable adjacency table does.

- **vs. the Genetic Algorithm already in this repo.** `tools/pcg_benchmark/pcg_benchmark/
  genetic.py`'s `evolve_content` is a solid, already-implemented, genuinely
  genre-agnostic baseline — it works purely against the generic `Space`/`contentSwap`
  interface, exactly the same generality argument as WFC. The difference is *how* each
  reaches a result: GA is a stochastic search over whole candidate grids, scored after the
  fact by `quality`/`controlability`, with no guarantee any intermediate individual is
  locally coherent. This repo's own `RUNNER.md` says as much about the hardest problem:
  *"`smbtile` (Mario): ... won't reliably reach full quality with the defaults — bump
  `--population-size`/`--generations`... if you need stronger Mario baselines."* WFC
  instead builds the grid so that **local adjacency is satisfied at every single step by
  construction** — a cell is never placed if it violates its neighbors' constraints — so it
  doesn't need generations of trial-and-error to reach structural coherence, and per-sample
  it is typically far cheaper than a population-based search.

- **Directly extends to controllability**, which matters because `pcg_benchmark` scores
  generators on a `controlability` metric (does the level hit a target tile/enemy/item
  count). Chen et al.'s controllable-WFC work (ICEC 2020) adds exactly this on top of
  vanilla WFC via three mechanisms — global constraints, multi-layer generation, and
  distance constraints — reporting improved manual control over the vanilla algorithm.
  Weighted tile frequencies (already part of vanilla WFC's collapse step) are the simplest
  version of this: bias a tile's weight up/down to push generated counts toward a target
  before generation, then repair afterward if needed.

- **A natural next step, not proposed here but worth flagging**: a 2025 hybrid approach
  (Springer, *Hybrid Procedural Level Generation Using Wave Function Collapse and Genetic
  Algorithms*) uses WFC for structural coherence and a GA for gameplay-objective
  optimization on top, reporting 56% faster convergence and 73% fewer unplayable levels
  than either method alone. Since this repo already has the GA half built
  (`genetic.py`), a WFC generator that seeds/constrains the GA's population (rather than
  starting from pure noise, which is what `evolve_content`'s
  `env.content_space.sample()` does today) is a plausible follow-up.

### Limitation to be upfront about

WFC is a great fit for the dense rectangular-grid problems (`binary`, `zelda`, `sokoban`,
`mdungeons`, `ddave`, `loderunnertile`, `smbtile`), which is most of the benchmark, but it
does **not** naturally cover the dict/graph-shaped problems — `isaac`'s content is a
`DictionarySpace` of a flat tile layout *plus* separate integer indices (`start`, `boss`,
`shop`, `treasure`), `building` is an `ArraySpace` of `{x, y, type}` dicts rather than a
dense grid, and `talakat` is a grid of spawner *parameters*, not spatial tiles. Those would
need either an adapted graph-WFC variant (see the graph-based WFC reference below) or a
different technique entirely — this document does not claim WFC is literally universal
across every problem in the benchmark, only across every problem expressible as a dense
tile grid, which is the brief given ("as long as they can be expressed as a grid").

## Implementation guide, grounded in `pcg_benchmark`

The framework's own generator contract (`pcg_benchmark/pcg_benchmark/README.md`,
"Creating a Generator") only requires: a way to sample/construct a valid `content` for
`env.content_space`, and the evaluation functions `env.info/quality/diversity/
controlability`. A WFC generator plugs into that contract as a *replacement for
`env.content_space.sample()`* — instead of pure noise, it returns a content grid that is
already locally coherent by construction, which then gets scored exactly like any other
generator's output (including as a seed population for the existing GA, per the hybrid
approach above).

### 1. Build the adjacency table

**Hand-authored (simple tiled model)** — works immediately from the tile legend already in
every problem's README, e.g. for `zelda` (`probs/zelda/README.md`): tiles
`0=wall, 1=empty, 2=player, 3=key, 4=door, 5=enemy`. You write down, per direction
(up/down/left/right), which tile IDs may be adjacent — e.g. `player`/`key`/`door` should
each appear exactly once and typically sit on `empty`, `enemy` should not be adjacent to
`player`, etc.

**Learned (overlapping model)** — extract rules automatically from a corpus of
known-good levels instead of hand-authoring them. This repo already has a natural source
for that corpus: maps produced by `genetic.py`'s `evolve_content` that pass
`env.quality(info) >= 1`. Sketch:

```python
from collections import defaultdict
import numpy as np

# corpus: list of 2D int arrays (each a valid content grid for one problem)
def learn_adjacency(corpus):
    # weights[tile] = frequency; allowed[(tile, dx, dy)] = set of tiles that may sit at (dx, dy)
    weights = defaultdict(int)
    allowed = defaultdict(set)
    neighbors = [(1, 0), (-1, 0), (0, 1), (0, -1)]
    for grid in corpus:
        grid = np.array(grid)
        h, w = grid.shape
        for y in range(h):
            for x in range(w):
                t = int(grid[y, x])
                weights[t] += 1
                for dx, dy in neighbors:
                    nx, ny = x + dx, y + dy
                    if 0 <= nx < w and 0 <= ny < h:
                        allowed[(t, dx, dy)].add(int(grid[ny, nx]))
    return weights, allowed
```

### 2. Core collapse/propagate loop, against `ArraySpace((h, w), IntegerSpace(n))`

`pcg_benchmark.probs.<problem>.problem.<Problem>` sets `self._content_space =
ArraySpace((self._height, self._width), IntegerSpace(n))` for every dense-grid problem
(see `pcg_benchmark/pcg_benchmark/spaces/array.py` — `ArraySpace(dimensions, value)`
nests `value` recursively into a `dimensions`-shaped array). A WFC generator targets that
same `(height, width, n)` shape:

```python
import numpy as np

def wfc_generate(height, width, n_tiles, weights, allowed, rng, max_restarts=50):
    neighbors = [(1, 0), (-1, 0), (0, 1), (0, -1)]

    for _ in range(max_restarts):
        # domain[y][x] = set of still-possible tile IDs for that cell
        domain = [[set(range(n_tiles)) for _ in range(width)] for _ in range(height)]
        collapsed = set()
        contradiction = False

        def entropy(y, x):
            d = domain[y][x]
            total = sum(weights.get(t, 1) for t in d)
            return -sum((weights.get(t, 1) / total) * np.log(weights.get(t, 1) / total)
                        for t in d) if len(d) > 1 else -1  # already-collapsed cells sort last

        while len(collapsed) < height * width and not contradiction:
            candidates = [(y, x) for y in range(height) for x in range(width)
                          if (y, x) not in collapsed]
            y, x = min(candidates, key=lambda c: entropy(*c))
            options = list(domain[y][x])
            probs = np.array([weights.get(t, 1) for t in options], dtype=float)
            tile = rng.choice(options, p=probs / probs.sum())
            domain[y][x] = {tile}
            collapsed.add((y, x))

            queue = [(y, x)]
            while queue:
                cy, cx = queue.pop()
                (ct,) = domain[cy][cx] if len(domain[cy][cx]) == 1 else (None,)
                for dx, dy in neighbors:
                    nx, ny = cx + dx, cy + dy
                    if not (0 <= nx < width and 0 <= ny < height):
                        continue
                    if (ny, nx) in collapsed:
                        continue
                    allowed_next = set()
                    for t in domain[cy][cx]:
                        allowed_next |= allowed.get((t, dx, dy), set())
                    new_domain = domain[ny][nx] & allowed_next
                    if len(new_domain) == 0:
                        contradiction = True
                        break
                    if new_domain != domain[ny][nx]:
                        domain[ny][nx] = new_domain
                        queue.append((ny, nx))
                if contradiction:
                    break

        if not contradiction:
            return np.array([[next(iter(domain[y][x])) for x in range(width)]
                              for y in range(height)])
    raise RuntimeError("WFC failed to converge within max_restarts")
```

This returns a plain `height x width` int array — a value directly valid for that
problem's `content_space` (`ArraySpace((height, width), IntegerSpace(n))`), so it can be
passed straight to `env.info(content)` / `env.quality(info)` / `env.render(content)` like
any other generator's output.

### 3. Steering toward `control_space` targets

`pcg_benchmark`'s `controlability` metric checks whether generated content hits a target
(e.g. `zelda`'s `{"player_key": 14, "key_door": 11}`, or `smbtile`'s enemy/jump/coin
counts). Two WFC-native levers, per the controllable-WFC mechanisms cited above:

- **Weight biasing** — before generation, scale `weights[tile]` up/down for tiles tied to
  the controlled quantity (e.g. raise the `enemy` tile's weight to push the generated
  enemy count toward a higher control target).
- **Post-pass repair** — after a successful collapse, if the count is still off-target,
  resample a small number of already-collapsed cells whose tile matches/could match the
  controlled tile type, then re-run propagation locally from just those cells (much cheaper
  than a full restart).

### 4. Where this replaces vs. complements the existing baseline

Drop-in replacement for `env.content_space.sample()` inside `genetic.py`'s initial
population (`evolve_content`'s population is currently seeded from pure random
`content_space.sample()` calls) — seeding from WFC output instead should start the GA from
already-locally-coherent grids instead of noise, which is the mechanism the hybrid
WFC+GA paper below reports gains from.

## References

**WFC and its predecessor**
- Gumin, M. (2016). *WaveFunctionCollapse* — reference implementation and README explaining
  the algorithm and the simple-tiled vs. overlapping models:
  https://github.com/mxgmn/WaveFunctionCollapse
- Merrell, P. (2007). *Example-based Model Synthesis* (i3D) — the direct predecessor to
  WFC, generating 2D/3D content from examples via the same
  collapse-and-propagate idea: https://paulmerrell.org/model-synthesis/ ;
  reference implementation: https://github.com/merrell42/model-synthesis
- Merrell, P. (2021). *Comparing Model Synthesis and Wave Function Collapse* — direct
  side-by-side of the two algorithms, from Model Synthesis's own author:
  https://paulmerrell.org/wp-content/uploads/2021/07/comparison.pdf
- *Model synthesis* — Wikipedia overview and terminology:
  https://en.wikipedia.org/wiki/Model_synthesis

**Algorithm explainers (implementation detail)**
- Boris the Brave. *Wave Function Collapse Explained*:
  https://www.boristhebrave.com/2020/04/13/wave-function-collapse-explained/
- Boris the Brave. *Wave Function Collapse tips and tricks* (practical backtracking /
  performance notes): https://www.boristhebrave.com/2020/02/08/wave-function-collapse-tips-and-tricks/
- Heaton, R. (2018). *The Wavefunction Collapse Algorithm explained very clearly*:
  https://robertheaton.com/2018/12/17/wavefunction-collapse-algorithm/
- gridbugs.org. *Procedural Generation with Wave Function Collapse*:
  https://www.gridbugs.org/wave-function-collapse/

**Formal framing**
- Karth, I. & Smith, A. M. (2017). *WaveFunctionCollapse is Constraint Solving in the Wild*
  (FDG) — positions WFC formally as constraint satisfaction, discussed via:
  https://blog.ptidej.net/procedural-generation-using-wave-function-collapse/

**Controllable / extended WFC**
- Chen, H. et al. (2020). *Automatic Generation of Game Levels Based on Controllable Wave
  Function Collapse Algorithm* (ICEC 2020) — global constraint, multi-layer generation, and
  distance-constraint mechanisms for steering WFC output:
  https://link.springer.com/chapter/10.1007/978-3-030-65736-9_3 ;
  https://inria.hal.science/hal-03686007
- *Automatic Generation of Game Content using a Graph-based Wave Function Collapse
  Algorithm* (IEEE, 2019) — extends WFC beyond dense rectangular grids to graph-structured
  content, relevant to this repo's non-grid problems (`isaac`, `building`):
  https://ieeexplore.ieee.org/document/8848019/
- *Hybrid Procedural Level Generation Using Wave Function Collapse and Genetic Algorithms*
  (Springer, 2025) — WFC for structure + GA for gameplay-objective optimization; reports
  56% faster convergence and 73% fewer unplayable levels than either method alone:
  https://link.springer.com/chapter/10.1007/978-3-031-97564-6_9

**Survey grounding (for the CA/GA/WFC comparison)**
- *Procedural Content Generation in Games: A Survey with Insights on Emerging Trends*
  (AIIDE 2024) — source of the "CA lacks control and cannot be easily adapted for
  generating other maps" characterization, and the search-based / constructive / ML
  taxonomy used above:
  https://wpsites.ucalgary.ca/serious-games/wp-content/uploads/sites/136/2024/09/2024aiide-PCG-in-games.pdf
- *A Survey of Search-Based Procedural Content Generation* (2023): https://arxiv.org/pdf/2311.04710
- Summerville, A. et al. (2018). *Procedural Content Generation via Machine Learning
  (PCGML)* — for the broader ML-based-PCG comparison point (heavier compute, needs
  training data, less directly controllable than WFC): https://ar5iv.labs.arxiv.org/html/1702.00539

**This repository's own framework**
- Khalifa, A., Gallota, R., Barthet, M., Liapis, A., Togelius, J., & Yannakakis, G. N.
  (2025). *The Procedural Content Generation Benchmark: An Open-source Testbed for
  Generative Challenges in Games* (FDG) — the paper behind the `pcg_benchmark` package
  this repo vendors as a submodule: https://arxiv.org/abs/2503.21474
- `pcg_benchmark/pcg_benchmark/README.md` — "Creating a Generator" section (generator
  contract: `content_space`, `contentSwap`, `env.info/quality/diversity/controlability`).
- `tools/pcg_benchmark/pcg_benchmark/genetic.py` — this repo's existing GA baseline
  generator, referenced above as the comparison point and the natural integration target
  for a WFC-seeded population.
- `tools/pcg_benchmark/pcg_benchmark/RUNNER.md` — source of the `smbtile` GA-baseline
  difficulty quote used in the "why WFC" comparison.
