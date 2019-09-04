#!/bin/sh

echo "Restoring Identity.API"
dotnet restore Identity.API/Identity.API.csproj

echo "Building Identity.API"
dotnet build Identity.API/Identity.API.csproj

echo "Publishing Identity.API"
dotnet publish Identity.API/Identity.API.csproj -c Release -o ./identity-api-release

echo "Moving output to root folder"
mv ./Identity.API/identity-api-release .