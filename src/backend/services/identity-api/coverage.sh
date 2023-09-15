#!/bin/bash

PATH="${PATH}:/root/.dotnet/tools"
dotnet tool install --global coverlet.console --version 1.2.1.0
dotnet tool install --global dotnet-reportgenerator-globaltool

generate_coverage(){
    coverlet ./Identity.API.UnitTests/output/Identity.API.UnitTests.dll \
    --target dotnet \
    --targetargs "vstest ./Identity.API.UnitTests/output/Identity.API.UnitTests.dll" \
    --format opencover \
    --output "./coverage.xml"
}

generate_report() {
    reportgenerator "-reports:./coverage.xml" "-targetdir:./coveragereport"
}

generate_coverage && generate_report