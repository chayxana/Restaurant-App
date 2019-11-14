#!/bin/sh

dotnet restore Menu.API.sln

dotnet test Menu.API.UnitTests/Menu.API.UnitTests.csproj \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=opencover

dotnet-sonarscanner begin \
     /d:"sonar.host.url=https://sonarcloud.io" \
     /o:"restaurant-app" \
     /k:"restaurant-menu-api" \
     /d:"sonar.login=77a854f90e4e5cf4f26de587be88715750a2a9cc" \
     /d:sonar.cs.opencover.reportsPaths="Menu.API.UnitTests/coverage.opencover.xml" \
     /d:sonar.coverage.exclusions="**Tests*.cs" \
     /d:sonar.pullrequest.branch=$CI_EXTERNAL_PULL_REQUEST_SOURCE_BRANCH_NAME \
     /d:sonar.pullrequest.key=$CI_EXTERNAL_PULL_REQUEST_IID

dotnet build Menu.API.sln
dotnet-sonarscanner end /d:sonar.login="77a854f90e4e5cf4f26de587be88715750a2a9cc"