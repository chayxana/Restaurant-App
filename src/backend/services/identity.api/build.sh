#!/bin/bash

srcdir=$( cd "$( dirname "${BASH_SOURCE[0]}" )" >/dev/null && pwd )
cd $srcdir

echo "Publishing Identity.API"
dotnet publish Identity.API/Identity.API.csproj -c Release -o ./identity-api-release