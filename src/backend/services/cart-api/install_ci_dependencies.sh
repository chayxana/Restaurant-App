#!/bin/sh

SONAR_SCANNER_VERSION=4.0.0.1744

apk add --no-cache git curl unzip openjdk8

java -version

curl --insecure -o ./sonarscanner.zip -L https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-${SONAR_SCANNER_VERSION}.zip && \
	unzip sonarscanner.zip && \
	rm sonarscanner.zip && \
	mv sonar-scanner-${SONAR_SCANNER_VERSION} /usr/lib/sonar-scanner && \
  ln -s /usr/lib/sonar-scanner/bin/sonar-scanner /usr/local/bin/sonar-scanner

sonar-scanner --help