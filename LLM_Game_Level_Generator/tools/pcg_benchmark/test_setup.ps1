# Quick test script to verify the benchmark setup works
# This creates a venv, installs dependencies, runs one iteration, and cleans up

param(
    [switch]$KeepVenv
)

$ErrorActionPreference = "Stop"

Write-Host "Testing PCG Benchmark Setup..." -ForegroundColor Cyan
Write-Host ""

# Test 1: Python availability
Write-Host "[1/4] Checking Python..." -ForegroundColor Yellow
try {
    $PythonVersion = python --version 2>&1
    Write-Host "  ✓ Python found: $PythonVersion" -ForegroundColor Green
} catch {
    Write-Host "  ✗ Python not found in PATH" -ForegroundColor Red
    exit 1
}

# Test 2: Create venv
Write-Host ""
Write-Host "[2/4] Creating test virtual environment..." -ForegroundColor Yellow
$TestVenv = ".\test_venv"
if (Test-Path $TestVenv) {
    Remove-Item -Path $TestVenv -Recurse -Force
}

try {
    python -m venv $TestVenv
    Write-Host "  ✓ Virtual environment created" -ForegroundColor Green
} catch {
    Write-Host "  ✗ Failed to create venv" -ForegroundColor Red
    exit 1
}

# Test 3: Activate and install
Write-Host ""
Write-Host "[3/4] Installing dependencies..." -ForegroundColor Yellow
$ActivateScript = Join-Path $TestVenv "Scripts\Activate.ps1"
& $ActivateScript

try {
    python -m pip install --upgrade pip --quiet
    python -m pip install -r requirements.txt --quiet
    Write-Host "  ✓ Dependencies installed successfully" -ForegroundColor Green
} catch {
    Write-Host "  ✗ Failed to install dependencies" -ForegroundColor Red
    deactivate
    if (!$KeepVenv) {
        Remove-Item -Path $TestVenv -Recurse -Force
    }
    exit 1
}

# Test 4: Run one test iteration
Write-Host ""
Write-Host "[4/4] Running test iteration..." -ForegroundColor Yellow
$TestOutput = ".\test_output"
New-Item -ItemType Directory -Path $TestOutput -Force | Out-Null

try {
    python LLM_Generator_Benchmark.py --iteration 1 --output "$TestOutput\test_result.json"
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✓ Test iteration completed successfully" -ForegroundColor Green
    } else {
        Write-Host "  ✗ Test iteration failed (exit code: $LASTEXITCODE)" -ForegroundColor Red
    }
} catch {
    Write-Host "  ✗ Test iteration encountered an error" -ForegroundColor Red
    Write-Host "     $($_.Exception.Message)" -ForegroundColor Red
}

# Cleanup
Write-Host ""
Write-Host "Cleaning up..." -ForegroundColor Yellow
deactivate
Start-Sleep -Seconds 1

if (!$KeepVenv) {
    Remove-Item -Path $TestVenv -Recurse -Force
    Write-Host "  ✓ Test venv removed" -ForegroundColor Green
} else {
    Write-Host "  ℹ Test venv kept at: $TestVenv" -ForegroundColor Cyan
}

if (Test-Path $TestOutput) {
    Remove-Item -Path $TestOutput -Recurse -Force
    Write-Host "  ✓ Test output removed" -ForegroundColor Green
}

Write-Host ""
Write-Host "Setup test complete! ✓" -ForegroundColor Green
Write-Host ""
Write-Host "To run the full benchmark:" -ForegroundColor Cyan
Write-Host "  .\run.ps1 -Iterations 10" -ForegroundColor White
