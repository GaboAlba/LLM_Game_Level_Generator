# Baseline Hyperparameter Tuning Log

Tuning record for the classical (non-LLM) baselines used in the **reasoning-model
(gpt-5.2, 20 candidates/map)** iterations: the genetic algorithm (`genetic.py`, `--baseline ga`)
and rule-based Wave Function Collapse (`wfc.py` with hand-authored adjacency rules from
`wfc_rules.py`, `--baseline wfc --wfc-rules hand`).

Goal: make each baseline as strong as possible, so the LLM is compared against a well-tuned
classical method rather than a default one, within a runtime that is practical for 420 maps
(21 variants x 20 maps).

## Method

- Harness: `tune.py <configs> <controls-per-variant> <results.jsonl>`. For every variant in
  `variants.py` it samples N control targets (`env.seed(1)`), then runs each configuration
  on each control with a fixed RNG seed (`1000 + control index`). All configurations see the
  same controls and seeds, so differences come from the hyperparameters alone.
- Every finished map is appended to the sweep's `.jsonl` as soon as it completes:
  `fit` (the search objective, `(quality + controlability) / 2`, from `genetic.fitness`),
  `q` (quality), `c` (controlability), `t` (CPU seconds for that one map).
- `summarize.py <results.jsonl>` prints the per-configuration means and a per-problem-family
  breakdown (`family:fitness/seconds-per-map`).
- Diversity is not optimised by either search (it is a set-level metric), so the tuning
  objective is quality + controllability only.
- 14 worker processes on a 16-core machine.

## Evaluation cost (random content, idle machine, 2026-11-03)

Cost of one `env.info` + fitness on randomly sampled content. Evolved or synthesised levels
can cost much more (solvers and agents explore further on near-playable levels), which is
what makes smbtile (Mario) and mdungeons the expensive families below.

| Variant | ms/eval | Variant | ms/eval | Variant | ms/eval |
|---|---|---|---|---|---|
| binary-v0 | 2.35 | loderunnertile-v0 | 5.45 | sokoban-v0 | 0.54 |
| binary-large-v0 | 8.60 | loderunnertile-gold-v0 | 5.49 | sokoban-large-v0 | 0.28 |
| binary-wide-v0 | 4.31 | loderunnertile-enemies-v0 | 5.93 | sokoban-complex-v0 | 1.49 |
| ddave-v0 | 0.86 | mdungeons-v0 | 0.85 | smbtile-v0 | 12.46 |
| ddave-complex-v0 | 0.84 | mdungeons-large-v0 | 3.18 | smbtile-medium-v0 | 6.46 |
| ddave-large-v0 | 1.95 | mdungeons-enemies-v0 | 0.85 | smbtile-small-v0 | 3.33 |
| | | | | zelda-v0 / enemies / large | 2.12 / 2.21 / 5.78 |

(`evaltime.py` reproduces this; run it on an idle machine, since concurrent load inflates the numbers roughly 4x.)

---

## Sweep 1 (2026-11-03): budget and mutation rate

Configs: `sweep1_configs.txt`. Raw results: `sweep1.jsonl`. 2 controls per variant (42 maps per config).

Starting points: the runner defaults, and the 100 generations x 100 candidates x 10 elites setting
used in reasoning iterations 5, 6 and 8.

| Config | Settings (changes from default) |
|---|---|
| ga_default | pop 24, gens 25, elites 2, mutation 0.08, polish 300 |
| ga_100x100 | pop 100, gens 100, elites 10 |
| ga_100x100_m02 | pop 100, gens 100, elites 10, mutation 0.02 |
| wfc_default | hand rules, polish 30, mutation 0.08, max restarts 20 |
| wfc_p500 | hand rules, polish 500 |
| wfc_p500_m02 | hand rules, polish 500, mutation 0.02 |

Wall time: 25,928 s (7.2 h). Note: CPU s/map are measured with 14 concurrent workers, so they
are somewhat higher than single-process timings, but they are comparable between configs.

### Results (mean over 42 maps)

| Config | Fitness | Quality | Control | CPU s/map | Max s/map |
|---|---|---|---|---|---|
| ga_default | 0.619 | 0.527 | 0.710 | 7 | 25 |
| ga_100x100 | 0.694 | 0.612 | 0.775 | 104 | 417 |
| **ga_100x100_m02** | **0.758** | 0.695 | 0.822 | 281 | 2,925 |
| wfc_default | 0.630 | 0.623 | 0.637 | 216 | 2,835 |
| **wfc_p500** | **0.820** | 0.844 | 0.797 | 1,950 | 25,266 |
| wfc_p500_m02 | 0.785 | 0.804 | 0.766 | 1,451 | 18,019 |

Fitness / CPU s per map, by problem family:

| Config | binary | ddave | loderunner | mdungeons | smbtile | sokoban | zelda |
|---|---|---|---|---|---|---|---|
| ga_default | 0.86 / 9 | 0.76 / 2 | 0.61 / 10 | 0.44 / 4 | 0.06 / 17 | 0.85 / 2 | 0.74 / 6 |
| ga_100x100 | 0.90 / 93 | 0.85 / 24 | 0.62 / 120 | 0.72 / 81 | 0.07 / 201 | 0.97 / 141 | 0.75 / 63 |
| ga_100x100_m02 | 0.97 / 83 | 0.84 / 29 | 0.62 / 122 | 1.00 / 1,214 | 0.08 / 202 | 0.89 / 266 | 0.92 / 51 |
| wfc_default | 0.76 / 0 | 0.81 / 2 | 0.22 / 0 | 0.52 / 1 | 0.70 / 1,512 | 0.81 / 0 | 0.60 / 0 |
| wfc_p500 | 0.90 / 4 | 0.93 / 41 | 0.34 / 4 | 0.84 / 22 | 0.80 / 13,418 | 0.98 / 158 | 0.95 / 3 |
| wfc_p500_m02 | 0.93 / 3 | 0.90 / 32 | 0.32 / 4 | 0.77 / 223 | 0.82 / 9,888 | 0.84 / 6 | 0.91 / 3 |

### Findings

1. **GA: mutation rate matters more than budget.** Going from 24x25 to 100x100 (about 16x the
   evaluations) gains +0.075 fitness. At the same budget, lowering per-gene mutation from 0.08 to 0.02
   gains another +0.064. 0.08 resamples about 8% of the tiles in every child, which destroys most of
   the structure a parent has built up on large maps. With 0.02, mdungeons reaches 1.00 and zelda goes
   from 0.75 to 0.92. sokoban drops from 0.97 to 0.89, so the lower rate is not best everywhere.
2. **GA cannot solve smbtile (Mario).** It stays at 0.06-0.09 fitness at every budget, because the
   all-or-nothing playability gate gives the search almost no gradient in a search space of about 10^2400.
   Extra budget is wasted there.
3. **WFC: polish steps are the main lever.** 30 to 500 steps takes overall fitness from 0.630 to 0.820,
   the largest single gain in the sweep. Most families are cheap (under 200 s/map), because WFC's
   adjacency-constrained output starts close to valid.
4. **WFC on smbtile is very strong but very expensive.** Hand rules reach 0.70-0.84 on Mario (vs about
   0.08 for GA), but every polish step simulates a playthrough of a nearly-playable level:
   about 13,400 CPU s (3.7 h) per smbtile map at 500 steps, vs 1,512 s at 30 steps.
5. **WFC mutation 0.02 vs 0.08 is mixed:** better and cheaper on smbtile (0.82 vs 0.80) and binary,
   but clearly worse on sokoban (0.84 vs 0.98) and mdungeons (0.77 vs 0.84). Overall, 0.08 is better (0.820 vs 0.785).
6. loderunnertile is WFC's weakest family (0.22-0.34), and GA does better there (0.61-0.62).

### Decision for sweep 2

- GA: keep mutation 0.02 as the new base. Test a bigger budget (200x200, 20 elites), an even lower
  mutation (0.01), a longer final polish (2,000 steps), and stronger selection (tournament 5).
- WFC: keep mutation 0.08. Test more polish (2,000 steps) and an intermediate mutation (0.05).
  smbtile is left out of WFC sweep 2: at 2,000 steps it would cost about 15 CPU-hours per map.
  Its setting is decided from the sweep 1 numbers (finding 4).

---

## Sweep 2 (2026-11-04): pushing the sweep 1 winners

Configs: `sweep2_ga_configs.txt`, `sweep2_wfc_configs.txt`. Raw results: `sweep2_ga.jsonl`,
`sweep2_wfc.jsonl`. Same controls and seeds as sweep 1, so sweep 1's `ga_100x100_m02` and `wfc_p500`
rows are reused as references rather than rerun (the runs are deterministic for a given seed).
WFC is run without smbtile (`tune.py ... smbtile`), so the WFC rows below are 36 maps, and the
sweep 1 references are re-averaged over the same 36 maps.

Wall time: WFC 2,355 s, GA 14,685 s (4.1 h).

### GA (42 maps; all at pop 100 / gens 100 / elites 10 unless stated)

| Config | Change vs ga_100x100_m02 | Fitness | Quality | Control | CPU s/map | Max s/map |
|---|---|---|---|---|---|---|
| ga_100x100 (sweep 1) | mutation 0.08 | 0.694 | 0.612 | 0.775 | 104 | 417 |
| ga_100x100_m02 (sweep 1) | reference | 0.758 | 0.695 | 0.822 | 281 | 2,925 |
| **ga_200x200_m02** | pop 200, gens 200, elites 20 | **0.793** | 0.742 | 0.845 | 884 | 7,648 |
| ga_100x100_m01 | mutation 0.01 | 0.786 | 0.729 | 0.843 | 639 | 4,508 |
| ga_100x100_m02_t5 | tournament 5 | 0.776 | 0.710 | 0.842 | 538 | 11,023 |
| ga_100x100_m02_p2000 | final polish 2,000 steps | 0.767 | 0.702 | 0.832 | 352 | 3,413 |

| Config | binary | ddave | loderunner | mdungeons | smbtile | sokoban | zelda |
|---|---|---|---|---|---|---|---|
| ga_100x100_m02 | 0.97 / 83 | 0.84 / 29 | 0.62 / 122 | 1.00 / 1,214 | 0.08 / 202 | 0.89 / 266 | 0.92 / 51 |
| ga_200x200_m02 | 0.99 / 249 | 0.91 / 129 | 0.62 / 462 | 1.00 / 3,682 | 0.09 / 757 | 0.98 / 787 | 0.96 / 124 |
| ga_100x100_m01 | 0.99 / 75 | 0.94 / 45 | 0.62 / 117 | 1.00 / 3,414 | 0.09 / 193 | 0.96 / 577 | 0.90 / 51 |
| ga_100x100_m02_t5 | 0.97 / 71 | 0.95 / 64 | 0.62 / 117 | 1.00 / 1,413 | 0.09 / 194 | 0.89 / 1,866 | 0.92 / 41 |
| ga_100x100_m02_p2000 | 0.99 / 88 | 0.86 / 37 | 0.62 / 134 | 1.00 / 1,673 | 0.09 / 220 | 0.89 / 262 | 0.92 / 49 |

### WFC, hand rules (36 maps, no smbtile)

| Config | Fitness | Quality | Control | CPU s/map | Max s/map |
|---|---|---|---|---|---|
| wfc_default (sweep 1) | 0.619 | 0.645 | 0.593 | 0.5 | 3 |
| wfc_p500 (sweep 1) | 0.824 | 0.883 | 0.766 | 39 | 363 |
| wfc_p500_m02 (sweep 1) | 0.778 | 0.830 | 0.727 | 45 | 1,298 |
| wfc_p500_m05 | 0.816 | 0.873 | 0.759 | 36 | 622 |
| **wfc_p2000** | **0.871** | 0.920 | 0.823 | 159 | 1,050 |
| wfc_p2000_m05 | 0.861 | 0.899 | 0.822 | 155 | 2,262 |

| Config | binary | ddave | loderunner | mdungeons | sokoban | zelda |
|---|---|---|---|---|---|---|
| wfc_p500 | 0.90 / 4 | 0.93 / 41 | 0.34 / 4 | 0.84 / 22 | 0.98 / 158 | 0.95 / 3 |
| wfc_p2000 | 0.94 / 13 | 0.95 / 153 | 0.38 / 17 | 0.97 / 425 | 0.99 / 344 | 1.00 / 4 |
| wfc_p500_m05 | 0.93 / 3 | 0.91 / 34 | 0.34 / 4 | 0.82 / 131 | 0.89 / 41 | 1.00 / 2 |
| wfc_p2000_m05 | 0.97 / 11 | 0.92 / 136 | 0.41 / 17 | 0.97 / 719 | 0.89 / 46 | 1.00 / 3 |

### Findings

1. **GA: every single change helps; budget and lower mutation help most.** 200x200 (+0.035) and
   mutation 0.01 (+0.028) are the two biggest gains. Tournament 5 (+0.018) and a longer final polish
   (+0.009) help less. The extra final-polish budget is mostly wasted, because the population has
   usually converged by then.
2. Mutation 0.01 is as strong as 0.02 on most families, but slow on mdungeons (3,414 s/map). The
   evolved dungeons get close to solvable, and the solver then explores further on every evaluation.
3. **GA on smbtile stays at about 0.09 in every configuration**, confirming sweep 1 finding 2. This is
   a property of the method on this problem, not a tuning gap.
4. **WFC: more polish keeps paying off, with diminishing returns.** 30 to 500 steps gives +0.205,
   and 500 to 2,000 gives +0.047. zelda reaches 1.00, and mdungeons goes from 0.84 to 0.97.
5. **WFC mutation 0.05 is not better than 0.08** (0.816 vs 0.824 at 500 steps, 0.861 vs 0.871 at
   2,000), and it collapses sokoban (0.89 vs 0.98-0.99). Keep 0.08.

### Decision for sweep 3

- GA: combine the winning changes (200x200 + mutation 0.01, with and without tournament 5), and try
  a bigger 300x300 budget at mutation 0.01, to find where the budget stops paying off.
- WFC: try 5,000 polish steps (no smbtile), to see whether the polish curve has flattened.
- WFC smbtile probe: run 2,000 polish steps on 1 control per smbtile variant (the same first control
  as sweep 1), to decide whether 2,000 steps is worth about 4x the CPU cost on Mario.

## Sweep 3 (2026-11-04 to 2026-11-05): combining winners, finding the plateau

Configs: `sweep3_ga_configs.txt`, `sweep3_wfc_configs.txt`, `sweep3_wfc_smb_configs.txt`.
Raw results: `sweep3_ga.jsonl`, `sweep3_wfc.jsonl`, `sweep3_wfc_smb.jsonl`. Same controls and seeds as
sweeps 1 and 2. All GA configs here use mutation 0.01; the other parameters are runner defaults
(crossover 0.7, tournament 3, final polish 300) unless stated.

Wall time: WFC p5000 3,111 s; smbtile probe 95,414 s (26.5 h, set by the single smbtile-v0 map);
GA about 22 h for the three configs.

### GA (42 maps)

| Config | Change | Fitness | Quality | Control | CPU s/map | Max s/map |
|---|---|---|---|---|---|---|
| ga_200x200_m02 (sweep 2) | reference | 0.793 | 0.742 | 0.845 | 884 | 7,648 |
| ga_200x200_m01 | 200x200, elites 20, mutation 0.01 | 0.845 | 0.821 | 0.870 | 1,665 | 20,724 |
| ga_200x200_m01_t5 | + tournament 5 | 0.876 | 0.851 | 0.901 | 5,270 | 90,023 |
| **ga_300x300_m01** | 300x300, elites 30, mutation 0.01 | **0.879** | 0.863 | 0.895 | 6,433 | 64,767 |

| Config | binary | ddave | loderunner | mdungeons | smbtile | sokoban | zelda |
|---|---|---|---|---|---|---|---|
| ga_200x200_m02 (sweep 2) | 0.99 / 249 | 0.91 / 129 | 0.62 / 462 | 1.00 / 3,682 | 0.09 / 757 | 0.98 / 787 | 0.96 / 124 |
| ga_200x200_m01 | 1.00 / 215 | 0.93 / 269 | 0.81 / 518 | 1.00 / 9,214 | 0.20 / 868 | 0.98 / 464 | 1.00 / 107 |
| ga_200x200_m01_t5 | 1.00 / 129 | 0.96 / 309 | 0.83 / 543 | 1.00 / 11,189 | 0.34 / 8,816 | 1.00 / 15,815 | 1.00 / 91 |
| ga_300x300_m01 | 1.00 / 296 | 0.93 / 593 | 0.86 / 1,209 | 1.00 / 19,220 | 0.36 / 22,616 | 1.00 / 959 | 1.00 / 141 |

### WFC, hand rules (36 maps, no smbtile)

| Config | Fitness | Quality | Control | CPU s/map | Max s/map |
|---|---|---|---|---|---|
| wfc_p2000 (sweep 2) | 0.871 | 0.920 | 0.823 | 159 | 1,050 |
| **wfc_p5000** | **0.889** | 0.939 | 0.840 | 452 | 3,060 |

| Config | binary | ddave | loderunner | mdungeons | sokoban | zelda |
|---|---|---|---|---|---|---|
| wfc_p2000 (sweep 2) | 0.94 / 13 | 0.95 / 153 | 0.38 / 17 | 0.97 / 425 | 0.99 / 344 | 1.00 / 4 |
| wfc_p5000 | 0.95 / 35 | 0.96 / 369 | 0.43 / 47 | 1.00 / 1,782 | 1.00 / 473 | 1.00 / 4 |

### WFC smbtile probe (1 control per smbtile variant, the same control as sweep 1)

| Variant | p500 fitness / CPU s (sweep 1) | p2000 fitness / CPU s |
|---|---|---|
| smbtile-small-v0 | 0.838 / 3,696 | 0.851 / 12,676 |
| smbtile-medium-v0 | 0.806 / 10,724 | 0.821 / 59,529 |
| smbtile-v0 | 0.796 / 22,357 | 0.827 / 95,414 |
| **Mean** | **0.813 / 12,259** | **0.833 / 55,873** |

### Findings

1. **The GA gains add up.** Combining 200x200 with mutation 0.01 gives 0.845 (+0.052 over the sweep 2
   best). Tournament 5 adds +0.031 on top of that, and 300x300 adds +0.034.
2. **GA on smbtile is not stuck after all.** Sweep 2 finding 3 is revised: with mutation 0.01 and a
   large budget, smbtile goes from 0.09 to 0.20 (200x200), 0.34 (with tournament 5) and 0.36
   (300x300). This is still far below WFC on Mario (about 0.81), but the low score was partly a
   tuning gap. With mutation 0.01, loderunner also improves, from 0.62 to 0.81-0.86.
3. **The GA is close to a plateau.** 300x300 beats 200x200 with tournament 5 by only 0.003, at about
   1.2x the mean CPU (tournament 5 has the worse single-map worst case: 90,023 s on one sokoban map). Nearly all of the remaining gain is on smbtile and loderunner. Every other family is at
   0.93-1.00.
4. **WFC polish is flattening.** Going from 2,000 to 5,000 steps gives +0.018 (from 500 to 2,000 it
   was +0.047), mostly on loderunner (+0.05) and mdungeons (reaching 1.00). The cost is still small
   (452 s/map) outside smbtile.
5. **More polish on smbtile WFC isn't worth it.** 2,000 steps gives +0.020 fitness at 4.6x the CPU,
   and a single smbtile-v0 map takes 26.5 h. 5,000 steps would take days per map.

### Final decision (used for reasoning iterations 11 and 12)

- **GA (iteration 11):** `ga_300x300_m01`, the best measured config: population 300, generations 300,
  elites 30, mutation 0.01, crossover 0.7, tournament 3, final polish 300. Expected cost for 420 maps:
  about 2.7 M CPU-s, about 54 h on 14 workers. Tournament 5 was not combined with 300x300, because
  that pairing is untested and tournament 5 multiplied the smbtile cost by 10.
- **WFC hand rules (iteration 12):** polish 5,000 steps, mutation 0.08, with a per-family override
  of **smbtile = 500 steps** (the new runner flag `--wfc-polish-overrides smbtile=500`). Using
  5,000 or 2,000 steps on smbtile would cost several days for at most +0.02. Expected cost: about
  0.97 M CPU-s, about 19 h.
- The runner change: `runner.py` now has `--wfc-polish-overrides FAMILY=N,...` (family = the make
  name before the first `-`). It changes only `polish_iterations` for those variants. The seed order
  is unchanged, and the value is recorded in `run_config.json`.

## GA corpus for the learned ("corpus-fed") WFC baseline (2026-11-05)

Goal (iteration 13): compare WFC that learns its adjacency rules from GA-evolved example maps with
WFC that uses hand-authored rules (iteration 12), and see whether a well-tuned GA corpus makes the
learned variant competitive. Previously the learned WFC evolved a throwaway corpus for every map
with a tiny GA (6 maps, population 12, 12 generations, polish 100).

- New script: `pcg_benchmark/build_corpus.py`. It evolves **6 maps per variant (126 in total)**
  once, with the tuned GA from sweep 3 (`ga_300x300_m01`: population 300, generations 300, elites 30,
  mutation 0.01, crossover 0.7, tournament 3, polish 300).
- The corpus uses its own seed, **2**, for both the controls and the RNG. The iterations use seed 1,
  so the example maps are never evolved for the exact control targets that WFC is scored on.
- Output: `tools/pcg_benchmark/corpus/ga_corpus_300x300_m01.json`, with a resumable `.jsonl`
  holding one map per line, plus each map's fitness. Build log: `corpus/build.log`.
- New runner flag: `--wfc-corpus-file PATH` (only with `--wfc-rules learned`). WFC learns each
  variant's rules from that variant's 6 corpus maps.
- Iteration 13 settings: the same as iteration 12 except for the rule source, so the comparison
  isolates that one change. Polish 5,000, smbtile override 500, mutation 0.08, seed 1.
- Expected corpus cost: about 0.8 M CPU-s, about 16-18 h on 14 workers.

## Precomputed baselines for iterations 11-13 (queued 2026-11-05)

Baselines depend only on the seeded controls, not on the LLM maps, so they are generated before the
gpt-5.2 run finishes. New runner flags: `--baselines-only --baseline-cache FILE
--maps-per-variant 20` saves the baselines (plus their controls and settings), and a later normal
run with `--baseline-cache FILE` skips generation. It also checks that the controls match and
records the cache's settings in `run_config.json`. Queue: `baseline_cache/run_queue.sh`, which
starts when the corpus is built and runs in the order below. Logs are in `baseline_cache/*.log`.

| Iteration | Cache file | Settings (all seed 1, 20 maps/variant, 14 workers) |
|---|---|---|
| 12 | `it12_wfc_hand.json` | WFC hand rules, polish 5,000 (smbtile 500), mutation 0.08 |
| 13 | `it13_wfc_corpus.json` | WFC learned from `corpus/ga_corpus_300x300_m01.json`, polish 5,000 (smbtile 500), mutation 0.08 |
| 11 | `it11_ga.json` | GA pop 300, gens 300, elites 30, mutation 0.01, crossover 0.7, tournament 3, polish 300 |
