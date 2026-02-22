# PCG Benchmark Runner for LLM Game Level Generator
# This script sets up a Python virtual environment, installs dependencies,
# runs the benchmark N times, and cleans up afterward.

param(
    [int]$Iterations = 10,
    [string]$OutputDir = ".\benchmark_results",
    [string]$VenvPath = ".\venv"
)

$ErrorActionPreference = "Stop"

# Get the script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ScriptDir

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "PCG Benchmark Runner" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Iterations: $Iterations" -ForegroundColor Yellow
Write-Host "Output Directory: $OutputDir" -ForegroundColor Yellow
Write-Host "Virtual Environment: $VenvPath" -ForegroundColor Yellow
Write-Host ""

# Step 1: Create virtual environment
Write-Host "[1/5] Creating Python virtual environment..." -ForegroundColor Green
if (Test-Path $VenvPath) {
    Write-Host "  -> Removing existing venv..." -ForegroundColor Gray
    Remove-Item -Path $VenvPath -Recurse -Force
}

try {
    python -m venv $VenvPath
    Write-Host "  -> Virtual environment created successfully" -ForegroundColor Green
} catch {
    Write-Host "ERROR: Failed to create virtual environment" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    exit 1
}

# Step 2: Activate virtual environment
Write-Host ""
Write-Host "[2/5] Activating virtual environment..." -ForegroundColor Green
$ActivateScript = Join-Path $VenvPath "Scripts\Activate.ps1"
if (!(Test-Path $ActivateScript)) {
    Write-Host "ERROR: Activation script not found at $ActivateScript" -ForegroundColor Red
    exit 1
}

# Activate the virtual environment
& $ActivateScript

# Verify activation
$PythonPath = (Get-Command python).Source
Write-Host "  -> Using Python: $PythonPath" -ForegroundColor Gray

# Step 3: Install dependencies
Write-Host ""
Write-Host "[3/5] Installing dependencies from requirements.txt..." -ForegroundColor Green
try {
    python -m pip install --upgrade pip --quiet
    python -m pip install -r requirements.txt --quiet
    Write-Host "  -> Dependencies installed successfully" -ForegroundColor Green
} catch {
    Write-Host "ERROR: Failed to install dependencies" -ForegroundColor Red
    Write-Host $_.Exception.Message -ForegroundColor Red
    deactivate
    Remove-Item -Path $VenvPath -Recurse -Force
    exit 1
}

# Step 4: Create output directory
Write-Host ""
Write-Host "[4/5] Setting up output directory..." -ForegroundColor Green
if (!(Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir | Out-Null
    Write-Host "  -> Created directory: $OutputDir" -ForegroundColor Gray
} else {
    Write-Host "  -> Using existing directory: $OutputDir" -ForegroundColor Gray
}

# Step 5: Run benchmark iterations
Write-Host ""
Write-Host "[5/5] Running benchmark iterations..." -ForegroundColor Green
$SuccessCount = 0
$FailureCount = 0
$StartTime = Get-Date

for ($i = 1; $i -le $Iterations; $i++) {
    $IterationStart = Get-Date
    Write-Host ""
    Write-Host "  Iteration $i of $Iterations" -ForegroundColor Cyan
    Write-Host "  ----------------------------------------" -ForegroundColor Gray

    try {
        # Run the Python benchmark script
        # Pass iteration number and output directory as arguments
        $ResultFile = Join-Path $OutputDir "benchmark_run_$i.json"
        python LLM_Generator_Benchmark.py --iteration $i --output $ResultFile

        if ($LASTEXITCODE -eq 0) {
            Write-Host "  -> SUCCESS: Iteration $i completed" -ForegroundColor Green
            $SuccessCount++
        } else {
            Write-Host "  -> FAILED: Iteration $i (exit code: $LASTEXITCODE)" -ForegroundColor Red
            $FailureCount++
        }
    } catch {
        Write-Host "  -> ERROR: Iteration $i encountered an exception" -ForegroundColor Red
        Write-Host "     $($_.Exception.Message)" -ForegroundColor Red
        $FailureCount++
    }

    $IterationEnd = Get-Date
    $IterationDuration = $IterationEnd - $IterationStart
    Write-Host "  -> Duration: $($IterationDuration.TotalSeconds) seconds" -ForegroundColor Gray
}

$EndTime = Get-Date
$TotalDuration = $EndTime - $StartTime

# Summary
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Benchmark Complete" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Total Iterations: $Iterations" -ForegroundColor Yellow
Write-Host "Successful: $SuccessCount" -ForegroundColor Green
Write-Host "Failed: $FailureCount" -ForegroundColor Red
Write-Host "Total Duration: $($TotalDuration.ToString('hh\:mm\:ss'))" -ForegroundColor Yellow
Write-Host "Average per iteration: $([math]::Round($TotalDuration.TotalSeconds / $Iterations, 2)) seconds" -ForegroundColor Yellow
Write-Host ""

# Step 6: Cleanup virtual environment
Write-Host "Cleaning up virtual environment..." -ForegroundColor Green
deactivate
Start-Sleep -Seconds 1  # Give time for deactivation
Remove-Item -Path $VenvPath -Recurse -Force
Write-Host "  -> Virtual environment removed" -ForegroundColor Green

Write-Host ""
Write-Host "Results saved to: $OutputDir" -ForegroundColor Cyan
Write-Host "Done!" -ForegroundColor Green
