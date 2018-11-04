#!/bin/bash

generate_coverage(){
    coverlet ./Menu.API.UnitTests.dll \
    --target dotnet \
    --targetargs "vstest ./Menu.API.UnitTests.dll" \
    --format opencover \
    --output "/app/coverage.xml"
}

generate_report() {
    reportgenerator "-reports:/app/coverage.xml" "-targetdir:/app/coveragereport"
}

publish_repoport_to_github_pages(){
    cd /app/coveragereport
    git init
    git config --global user.email "gitlabci@github.com"
    git config --global user.name "Gitlab_CI"
    git remote add origin https://github.com/Jurabek/restaurant-menu-api-coverage.git
    git pull
    git add -A
    DATE=`date +%Y-%m-%d`
    COMMIT_MESSAGE="Updating code coverage-$DATE"
    git commit -m"$COMMIT_MESSAGE"
    git push -f https://$GITHUB_USER_NAME:$GITHUB_USER_PASSWORD@github.com/Jurabek/restaurant-menu-api-coverage.git master
}

generate_coverage && generate_report && publish_repoport_to_github_pages