#!/usr/bin/env bash
set -euo pipefail

# -------- Settings --------
CONFIG=${CONFIG:-Release}
RESULTS_DIR=${RESULTS_DIR:-artifacts/test}
COVERAGE_DIR=${COVERAGE_DIR:-artifacts/coverage-html}
SLN=${SLN:-}   # optionally set SLN="path/to/Your.sln" before running

# -------- Locate solution if not provided --------
if [[ -z "${SLN}" ]]; then
  mapfile -t SLNS < <(find . -maxdepth 2 -name "*.sln")
  if [[ ${#SLNS[@]} -eq 0 ]]; then
    echo "❌ No .sln file found. Set SLN=... or run from a repo with a solution."
    exit 1
  fi
  SLN="${SLNS[0]}"
fi
echo "🧩 Using solution: $SLN"

# -------- Restore & Build --------
dotnet restore "$SLN"
dotnet build "$SLN" -c "$CONFIG" --no-restore

# -------- Test + collect Cobertura coverage --------
mkdir -p "$RESULTS_DIR"
dotnet test "$SLN" -c "$CONFIG" --no-build \
  --logger "trx;LogFileName=tests.trx" \
  --results-directory "$RESULTS_DIR" \
  --collect:"XPlat Code Coverage"

# -------- Ensure ReportGenerator is installed --------
if ! command -v reportgenerator >/dev/null 2>&1; then
  echo "⬇️  Installing ReportGenerator..."
  dotnet tool install -g dotnet-reportgenerator-globaltool
  export PATH="$PATH:$HOME/.dotnet/tools"
fi

# -------- Generate HTML report --------
mkdir -p "$COVERAGE_DIR"
reportgenerator \
  -reports:"$RESULTS_DIR/**/coverage.cobertura.xml" \
  -targetdir:"$COVERAGE_DIR" \
  -reporttypes:"Html"

echo "✅ HTML coverage report ready: $COVERAGE_DIR/index.html"
