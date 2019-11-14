#!/bin/sh
 
 ./gradlew sonarqube \
        -Dsonar.projectKey=restaurant-order-api \
        -Dsonar.organization=restaurant-app \
        -Dsonar.host.url=https://sonarcloud.io \
        -Dsonar.login=9c4bb9969c0a77dbb45d725b8979a49d0ecb28d7 \
        -Dsonar.pullrequest.branch=$CI_EXTERNAL_PULL_REQUEST_SOURCE_BRANCH_NAME \
        -Dsonar.pullrequest.key=$CI_EXTERNAL_PULL_REQUEST_IID