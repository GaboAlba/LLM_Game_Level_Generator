"""Tuning sweep: run GA / hand-rule WFC configs on N controls per variant, report fitness + time."""
import sys, time, json
import os; sys.path.insert(0, os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "pcg_benchmark"))
from concurrent.futures import ProcessPoolExecutor, as_completed
import numpy as np
import pcg_benchmark, genetic, wfc
from variants import VARIANTS

def work(task):
    name, kind, params, make, control, seed = task
    env = pcg_benchmark.make(make); env.seed(seed)
    rng = np.random.default_rng(seed)
    t0 = time.perf_counter()
    if kind == "ga":
        content, fit = genetic.evolve_content(env, control, rng=rng, **params)
    else:
        content, fit = wfc.generate_content(env, control, rng=rng, rules="auto", **params)
    info = env.info(content)
    q = env.quality(info)[1]; c = env.controlability(info, control)[1]
    return name, make, fit, q, c, time.perf_counter() - t0

if __name__ == "__main__":
    configs = json.loads(open(sys.argv[1]).read())
    per_variant = int(sys.argv[2]) if len(sys.argv) > 2 else 2
    # Optional 4th arg: comma-separated problem families to skip (e.g. "smbtile").
    exclude = set(sys.argv[4].split(",")) if len(sys.argv) > 4 else set()
    tasks = []
    for v in VARIANTS:
        if v.make.split("-")[0] in exclude:
            continue
        env = pcg_benchmark.make(v.make); env.seed(1)
        controls = [env.control_space.sample() for _ in range(per_variant)]
        for i, ctl in enumerate(controls):
            for name, (kind, params) in configs.items():
                tasks.append((name, kind, params, v.make, ctl, 1000 + i))
    out = open(sys.argv[3], "a", buffering=1)
    t0 = time.perf_counter()
    with ProcessPoolExecutor(14) as ex:
        for f in as_completed([ex.submit(work, t) for t in tasks]):
            name, make, fit, q, c, dt = f.result()
            out.write(json.dumps({"cfg": name, "make": make, "fit": fit, "q": q, "c": c, "t": dt}) + chr(10))
    print(f"wall {time.perf_counter()-t0:.0f}s")
    res = {}
    for line in open(sys.argv[3]):
        r = json.loads(line)
        if r["cfg"] in configs:
            res.setdefault(r["cfg"], {}).setdefault(r["make"], []).append((r["fit"], r["q"], r["c"], r["t"]))
    fam = lambda m: m.split("-")[0]
    for name in configs:
        r = res.get(name)
        if not r: continue
        allv = [x for v in r.values() for x in v]
        print(f"\n== {name}: fit {np.mean([x[0] for x in allv]):.3f}  Q {np.mean([x[1] for x in allv]):.3f}  C {np.mean([x[2] for x in allv]):.3f}  cpu-s/map {np.mean([x[3] for x in allv]):.1f} (max {max(x[3] for x in allv):.0f})")
        fams = {}
        for m, xs in r.items(): fams.setdefault(fam(m), []).extend(xs)
        print("   " + "  ".join(f"{k}:{np.mean([x[0] for x in xs]):.2f}/{np.mean([x[3] for x in xs]):.0f}s" for k, xs in sorted(fams.items())))
