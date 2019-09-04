#!/bin/sh

echo "Restoring Menu.API"
dotnet restore Menu.API/Menu.API.csproj

echo "Building Menu.API"
dotnet build Menu.API/Menu.API.csproj

echo "Publishing Menu.API"
dotnet publish Menu.API/Menu.API.csproj -c Release -o ./menu-api-release

echo "Moving output to root folder"
mv ./Menu.API/menu-api-release .