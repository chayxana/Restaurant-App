FROM openjdk:8-jdk-alpine as builder

COPY . /usr/src/discovery
WORKDIR /usr/src/discovery

RUN ./gradlew build

FROM java:alpine
COPY --from=builder /usr/src/discovery/build/libs/restaurant-service-discovery-0.0.1-SNAPSHOT.jar service-discovery.jar
ENTRYPOINT ["java", "-jar", "/service-discovery.jar"]
EXPOSE 8761