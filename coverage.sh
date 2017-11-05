#!/bin/bash

set -e

# Install OpenCover and ReportGenerator, and save the path to their executables.
./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
./.nuget/nuget.exe install -Verbosity quiet -OutputDirectory packages -Version 1.0.3 Codecov

OPENCOVER=$PWD/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe
CODECOV=$PWD/packages/Codecov.1.0.3/tools/codecov.exe

CONFIG=Release
# Arguments to use for the build
DOTNET_BUILD_ARGS="-c $CONFIG"
# Arguments to use for the test
DOTNET_TEST_ARGS="$DOTNET_BUILD_ARGS"
testdir=./Server/Tests
solution=Restaurant.Server.sln

echo CLI args: $DOTNET_BUILD_ARGS

echo Restoring

dotnet restore $solution

echo Building

dotnet build $solution $DOTNET_BUILD_ARGS 

echo Testing

coverage=./coverage
rm -rf $coverage
mkdir $coverage


dotnet test -f netcoreapp2.0 $DOTNET_TEST_ARGS $testdir/Restaurant.Server.Api.UnitTests/Restaurant.Server.Api.UnitTests.csproj

echo "Calculating coverage with OpenCover"
$OPENCOVER \
  -target:"c:\Program Files\dotnet\dotnet.exe" \
  -targetargs:"test -f netcoreapp2.0 -c $DOTNET_TEST_ARGS $testdir/Restaurant.Server.Api.UnitTests/Restaurant.Server.Api.UnitTests.csproj" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[Restaurant.Server.Api*]* -[Restaurant.Server.Api.UnitTests*]*" \
  -searchdirs:$testdir/Restaurant.Server.Api.UnitTests/bin/$CONFIG/netcoreapp2.0 \
  -register:user \
  -excludebyattribute:*.ExcludeFromCodeCoverage*

echo "Uploading coverage"
$CODECOV \
    -f $coverage/coverage.xml \
    -t 077aea61-7f42-49aa-b825-56c1e7c7095b