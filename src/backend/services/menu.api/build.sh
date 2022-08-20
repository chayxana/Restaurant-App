#!/bin/sh

srcdir=$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )
cd $srcdir

echo "Restoring Menu.API"
dotnet restore Menu.API/Menu.API.csproj

echo "Publishing Menu.API"
dotnet publish Menu.API/Menu.API.csproj -c Release -o ./menu-api-release
