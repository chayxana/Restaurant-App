FROM openjdk:8-jdk-alpine as builder

COPY . /usr/src/ordering_api
WORKDIR /usr/src/ordering_api

RUN ./gradlew build

FROM openjdk:8-jdk-alpine
WORKDIR /root/
COPY --from=builder /usr/src/ordering_api/build/libs/order.api.jar .

EXPOSE 8090
CMD ["java", "-jar", "./order.api.jar"]