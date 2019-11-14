FROM golang:1.13.4-alpine

ENV SONAR_SCANNER_VERSION=4.0.0.1744

RUN apk add --no-cache git curl unzip openjdk8-jre jq python py-pip
RUN pip install awscli
RUN apk del jq py-pip

RUN java -version

RUN curl --insecure -o ./sonarscanner.zip -L https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-${SONAR_SCANNER_VERSION}.zip && \
	unzip sonarscanner.zip && \
	rm sonarscanner.zip && \
	mv sonar-scanner-${SONAR_SCANNER_VERSION} /usr/lib/sonar-scanner && \
  ln -s /usr/lib/sonar-scanner/bin/sonar-scanner /usr/local/bin/sonar-scanner

RUN sonar-scanner --help