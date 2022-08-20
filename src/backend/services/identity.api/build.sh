#!/bin/bash

srcdir=$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )
cd $srcdir

echo "Restoring Identity.API"
dotnet restore Menu.API/Menu.API.csproj

echo "Building Identity.API"
dotnet build Identity.API/Identity.API.csproj

echo "Publishing Identity.API"
dotnet publish Identity.API/Identity.API.csproj -c Release -o ./identity-api-release