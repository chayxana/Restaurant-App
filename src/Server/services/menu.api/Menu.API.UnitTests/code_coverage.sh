#!/bin/bash

generate_coverage(){
    coverlet ./Menu.API.UnitTests.dll \
    --target dotnet \
    --targetargs "vstest ./Menu.API.UnitTests.dll" \
    --format opencover \
    --output "/app/coverage.xml"
}

generate_report() {
    reportgenerator "-reports:/app/coverage.xml" "-targetdir:/app/coveragereport"
}

generate_coverage && generate_report