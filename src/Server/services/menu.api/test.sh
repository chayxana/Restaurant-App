#!/bin/sh

echo "Restoring Menu.API.UnitTests"
dotnet restore Menu.API.UnitTests/Menu.API.UnitTests.csproj

echo "Building Menu.API.UnitTests"
dotnet build Menu.API.UnitTests/Menu.API.UnitTests.csproj -c Release -o ./output

echo "Testing Menu.API.UnitTests"
dotnet test Menu.API.UnitTests/Menu.API.UnitTests.csproj

echo "Code coverage Menu.API"
sh ./coverage.sh