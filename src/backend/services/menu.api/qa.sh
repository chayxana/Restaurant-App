#!/bin/sh

dotnet tool install --global dotnet-sonarscanner --version 4.8.0

dotnet restore Menu.API.sln

dotnet test Menu.API.UnitTests/Menu.API.UnitTests.csproj \
    /p:CollectCoverage=true \
    /p:CoverletOutputFormat=opencover

dotnet-sonarscanner begin \
    /d:"sonar.host.url=https://sonarcloud.io" \
    /o:"restaurant-app" \
    /k:"restaurant-menu-api" \
    /d:"sonar.login=$SONAR_TOKEN" \
    /d:sonar.cs.opencover.reportsPaths="Menu.API.UnitTests/coverage.opencover.xml" \
    /d:sonar.coverage.exclusions="**Tests*.cs" \
    /d:sonar.exclusions="Menu.API/Migrations/**, Menu.API/Data/**, Menu.API/Mapper/**"
    /d:sonar.github.repository=Jurabek/Restaurant-App \
    /d:sonar.pullrequest.provider=GitHub

dotnet build Menu.API.sln
dotnet-sonarscanner end /d:sonar.login=$SONAR_TOKEN