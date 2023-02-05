FROM quay.io/quarkus/ubi-quarkus-graalvmce-builder-image:22.3.0-java17 AS build

USER root
RUN microdnf install findutils

COPY --chown=quarkus:quarkus gradlew /code/gradlew
COPY --chown=quarkus:quarkus gradle /code/gradle
COPY --chown=quarkus:quarkus build.gradle /code/
COPY --chown=quarkus:quarkus settings.gradle /code/
COPY --chown=quarkus:quarkus gradle.properties /code/
USER quarkus

WORKDIR /code
COPY src /code/src

RUN ./gradlew build -Dquarkus.package.type=native -Dquarkus.native.native-image-xmx=8g -Dquarkus-profile=local

## Stage 2 : create the docker final image
FROM quay.io/quarkus/quarkus-micro-image:2.0
WORKDIR /work/
COPY --from=build /code/build/*-runner /work/application
RUN chmod 775 /work
EXPOSE 8080
CMD ["./application", "-Dquarkus.http.host=0.0.0.0"]