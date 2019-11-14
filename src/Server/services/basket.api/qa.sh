CGO_ENABLED=0 go test ./controllers ./repositories ./eureka ./oidc --cover -coverprofile=coverage.out

sonar-scanner -Dsonar.login=423f19c6b4a3ad087de2e7039311d3f047bcb90e \
    -Dsonar.pullrequest.branch=$CI_EXTERNAL_PULL_REQUEST_SOURCE_BRANCH_NAME \
    -Dsonar.pullrequest.key=$CI_EXTERNAL_PULL_REQUEST_IID