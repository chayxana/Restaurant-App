FROM quay.io/quarkus/ubi-quarkus-graalvmce-builder-image:22.3.0-java17 AS build

USER root
RUN microdnf install findutils

COPY --chown=quarkus:quarkus mvnw /code/mvnw
COPY --chown=quarkus:quarkus .mvn /code/.mvn
COPY --chown=quarkus:quarkus pom.xml /code/

USER quarkus

WORKDIR /code
RUN --mount=type=cache,target=/root/.m2 ./mvnw -B org.apache.maven.plugins:maven-dependency-plugin:3.1.2:go-offline
COPY src /code/src

RUN --mount=type=cache,target=/root/.m2 ./mvnw package -Pnative -Dquarkus.package.type=native -Dquarkus.native.native-image-xmx=8g -Dquarkus.profile=local

## Stage 2 : create the docker final image
FROM quay.io/quarkus/quarkus-micro-image:2.0
WORKDIR /work/
COPY --from=build /code/target/*-runner /work/application
RUN chmod 775 /work
EXPOSE 8080
CMD ["./application", "-Dquarkus.http.host=0.0.0.0"]