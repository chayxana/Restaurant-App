# FROM openjdk:8-jdk-alpine AS builder

# COPY . /usr/src/gateway_api
# WORKDIR /usr/src/gateway_api

# RUN ./gradlew build -x test

# FROM openjdk:8-jdk-alpine
# WORKDIR /root/
# COPY --from=builder /usr/src/gateway_api/build/libs/restaurant-gateway-0.0.1.jar .

# EXPOSE 8080
# CMD ["java", "-jar", "./restaurant-gateway-0.0.1.jar"]

FROM openjdk:8-jdk-alpine

ADD build/libs/restaurant-gateway-0.0.1.jar gateway.jar
ENTRYPOINT ["java", "-jar", "/gateway.jar"]
EXPOSE 8080