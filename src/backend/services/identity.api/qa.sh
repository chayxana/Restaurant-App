#!/bin/sh

dotnet tool install --global dotnet-sonarscanner --version 4.8.0

dotnet restore Identity.API.sln

dotnet test Identity.API.UnitTests/Identity.API.UnitTests.csproj \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=opencover

dotnet-sonarscanner begin \
    /d:"sonar.host.url=https://sonarcloud.io" \
    /o:"restaurant-app" \
    /k:"restaurant-identity-api" \
    /d:"sonar.login=77a854f90e4e5cf4f26de587be88715750a2a9cc" \
    /d:sonar.cs.opencover.reportsPaths="Identity.API.UnitTests/coverage.opencover.xml" \
    /d:sonar.coverage.exclusions="**Tests*.cs" \
    /d:sonar.exclusions="Identity.API/wwwroot/**, Identity.API/Migrations/**"
    /d:sonar.github.repository=Jurabek/Restaurant-App \
    /d:sonar.pullrequest.base=$CI_EXTERNAL_PULL_REQUEST_TARGET_BRANCH_NAME \
    /d:sonar.pullrequest.branch=$CI_EXTERNAL_PULL_REQUEST_SOURCE_BRANCH_NAME \
    /d:sonar.pullrequest.key=$CI_EXTERNAL_PULL_REQUEST_IID \
    /d:sonar.pullrequest.provider=GitHub

dotnet build Identity.API.sln
dotnet-sonarscanner end /d:sonar.login="77a854f90e4e5cf4f26de587be88715750a2a9cc"