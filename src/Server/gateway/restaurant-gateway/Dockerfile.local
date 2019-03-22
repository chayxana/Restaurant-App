FROM openjdk:8-jdk-alpine as builder

COPY . /usr/src/gateway
WORKDIR /usr/src/gateway

RUN ./gradlew build

FROM openjdk:8-jdk-alpine
COPY --from=builder /usr/src/gateway/build/libs/restaurant-gateway-0.0.1.jar gateway.jar

ENTRYPOINT ["java", "-jar", "/gateway.jar"]
EXPOSE 8080