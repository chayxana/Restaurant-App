#!/bin/sh

echo "Restoring Identity.API.UnitTests"
dotnet restore Identity.API.UnitTests/Identity.API.UnitTests.csproj

echo "Building Identity.API.UnitTests"
dotnet build Identity.API.UnitTests/Identity.API.UnitTests.csproj -c Release -o ./output

echo "Testing Identity.API.UnitTests"
dotnet test Identity.API.UnitTests/Identity.API.UnitTests.csproj

echo "Code coverage Identity.API"
sh ./coverage.sh