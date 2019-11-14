#!/bin/sh
 
 ./gradlew sonarqube \
        -Dsonar.projectKey=restaurant-order-api \
        -Dsonar.organization=restaurant-app \
        -Dsonar.host.url=https://sonarcloud.io \
        -Dsonar.login=9c4bb9969c0a77dbb45d725b8979a49d0ecb28d7 \
        -Dsonar.pullRequest=$CI_EXTERNAL_PULL_REQUEST_IID \
        -Dsonar.analysis.mode=preview \
        -Dsonar.github.repository=Jurabek/Restaurant-App \
        -Dsonar.github.oauth=$GITHUB_SONAR_KEY