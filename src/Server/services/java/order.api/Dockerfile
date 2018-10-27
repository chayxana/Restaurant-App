FROM gradle:4.2.1-jdk8-alpine AS builder

COPY . /usr/src/ordering_api
WORKDIR /usr/src/ordering_api

USER root
RUN chown -R gradle /usr/src/ordering_api
USER gradle
RUN gradle build --stacktrace

FROM java:alpine
WORKDIR /root/
COPY --from=builder /usr/src/ordering_api/build/libs/order.api-0.0.1-SNAPSHOT.jar .

EXPOSE 5300
ENTRYPOINT ["java", "-jar", "./order.api-0.0.1-SNAPSHOT.jar"]