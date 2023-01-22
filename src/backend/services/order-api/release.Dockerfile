####
# This Dockerfile is used in order to build a container that runs the Quarkus application in native (no JVM) mode.
#
# Before building the container image run:
#
# ./gradlew build -Dquarkus.package.type=native
#
# Then, build the image with:
#
# docker build -f src/main/docker/Dockerfile.native -t quarkus/order-api .
#
# Then run the container using:
#
# docker run -i --rm -p 8080:8080 quarkus/order-api
#
###
FROM registry.access.redhat.com/ubi8/ubi-minimal:8.5

ARG sourceFilePath=target/*-runner

WORKDIR /work/
RUN chown 1001 /work \
    && chmod "g+rwX" /work \
    && chown 1001:root /work
COPY --chown=1001:root ${sourceFilePath} /work/application

EXPOSE 8080
USER 1001

CMD ["./application", "-Dquarkus.http.host=0.0.0.0"]
