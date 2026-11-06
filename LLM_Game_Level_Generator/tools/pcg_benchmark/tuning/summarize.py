import sys, json, numpy as np, collections
rows=[json.loads(l) for l in open(sys.argv[1])]
by=collections.defaultdict(list)
for r in rows: by[r["cfg"]].append(r)
for cfg, rs in by.items():
    fams=collections.defaultdict(list)
    for r in rs: fams[r["make"].split("-")[0]].append(r)
    print(f"{cfg:18s} n={len(rs):3d} fit {np.mean([r['fit'] for r in rs]):.3f} Q {np.mean([r['q'] for r in rs]):.3f} C {np.mean([r['c'] for r in rs]):.3f} s/map {np.mean([r['t'] for r in rs]):6.1f} max {max(r['t'] for r in rs):6.0f}")
    print("    " + "  ".join(f"{k}:{np.mean([r['fit'] for r in v]):.2f}/{np.mean([r['t'] for r in v]):.0f}s(n{len(v)})" for k, v in sorted(fams.items())))
