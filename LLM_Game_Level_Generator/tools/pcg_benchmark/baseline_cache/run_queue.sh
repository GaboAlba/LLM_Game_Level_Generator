#!/bin/bash
# Precompute the reasoning-track baselines (iterations 11-13) for 20 maps/variant, seed 1.
#   1. builds (or resumes) the GA corpus for the corpus-fed WFC (corpus/*.jsonl),
#   2. runs the three baselines one after another, each into a cache file here.
# `runner.py --iteration N --seed 1 --baseline ... --baseline-cache <file>` later reuses a
# cache once the gpt-5.2 maps are ready. See tuning/TUNING_LOG.md for the settings.
#
# Usage: WORKERS=14 PYTHON=python3 bash baseline_cache/run_queue.sh
set -u
ROOT="$(cd "$(dirname "$0")/.." && pwd)"
CACHE="$ROOT/baseline_cache"
CORPUS="$ROOT/corpus/ga_corpus_300x300_m01.json"
WORKERS="${WORKERS:-14}"
PYTHON="${PYTHON:-python}"
cd "$ROOT/pcg_benchmark"

if [ ! -f "$CORPUS" ]; then
    echo "corpus build: start $(date)"
    "$PYTHON" -u build_corpus.py "$CORPUS" --workers "$WORKERS" >> "$ROOT/corpus/build.log" 2>&1 \
        || { echo "corpus build failed, see corpus/build.log"; exit 1; }
    echo "corpus build: done $(date)"
fi

COMMON="--seed 1 --workers $WORKERS --baselines-only --maps-per-variant 20"
WFC="--wfc-polish-iterations 5000 --wfc-polish-overrides smbtile=500 --mutation-rate 0.08"

run() {  # run NAME ARGS... -- skips a cache that already exists
    local name=$1; shift
    if [ -f "$CACHE/$name.json" ]; then echo "$name: cached, skipping"; return; fi
    echo "$name: start $(date)"
    "$PYTHON" -u runner.py "$@" $COMMON --baseline-cache "$CACHE/$name.json" > "$CACHE/$name.log" 2>&1
    echo "$name: done (exit $?) $(date)"
}

run it12_wfc_hand   --baseline wfc --wfc-rules hand $WFC
run it13_wfc_corpus --baseline wfc --wfc-rules learned --wfc-corpus-file "$CORPUS" $WFC
run it11_ga         --baseline ga --population-size 300 --generations 300 --elite-count 30 --mutation-rate 0.01
