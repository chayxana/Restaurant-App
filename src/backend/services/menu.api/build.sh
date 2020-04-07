#!/bin/sh

echo "Publishing Menu.API"
dotnet publish Menu.API/Menu.API.csproj -c Release -o ./menu-api-release
