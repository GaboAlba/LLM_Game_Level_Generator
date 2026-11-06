import sys, time
sys.path.insert(0, r"C:/Github/LLM_Game_Level_Generator/LLM_Game_Level_Generator/tools/pcg_benchmark/pcg_benchmark")
from concurrent.futures import ProcessPoolExecutor
import pcg_benchmark, genetic
from variants import VARIANTS

def t(make):
    env = pcg_benchmark.make(make); env.seed(1)
    c = env.control_space.sample()
    n = 0; t0 = time.perf_counter()
    while time.perf_counter() - t0 < 3 or n < 3:
        info = env.info(env.content_space.sample()); genetic.fitness(env, info, c); n += 1
    return make, (time.perf_counter() - t0) / n * 1000

if __name__ == "__main__":
    with ProcessPoolExecutor(8) as ex:
        for make, ms in ex.map(t, [v.make for v in VARIANTS]):
            print(f"{make:28s} {ms:8.2f} ms/eval")
