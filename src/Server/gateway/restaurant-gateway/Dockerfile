FROM java:alpine AS builder

COPY . /usr/src/gateway_api
WORKDIR /usr/src/gateway_api

RUN ./gradlew build -x test

FROM java:alpine
WORKDIR /root/
COPY --from=builder /usr/src/gateway_api/build/libs/restaurant-gateway-0.0.1.jar .

EXPOSE 8080
CMD ["java", "-jar", "./restaurant-gateway-0.0.1.jar"]