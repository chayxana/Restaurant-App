CGO_ENABLED=0 go test ./controllers ./repositories ./eureka ./oidc --cover -coverprofile=coverage.out

sonar-scanner -Dsonar.login=423f19c6b4a3ad087de2e7039311d3f047bcb90e \
    -Dsonar.pullRequest=$CI_EXTERNAL_PULL_REQUEST_IID \
    -Dsonar.analysis.mode=preview \
    -Dsonar.github.repository=Jurabek/Restaurant-App \
    -Dsonar.github.oauth=$GITHUB_SONAR_KEY