# FROM openjdk:8-jdk-alpine AS builder

# COPY . /usr/src/service-discovery
# WORKDIR /usr/src/service-discovery

# RUN ./gradlew build -x test

# FROM java:alpine
# WORKDIR /root/
# COPY --from=builder /usr/src/service-discovery/build/libs/restaurant-service-discovery-0.0.1-SNAPSHOT.jar .

# EXPOSE 8761
# CMD ["java", "-jar", "./restaurant-service-discovery-0.0.1-SNAPSHOT.jar"]


FROM java:alpine

ADD build/libs/restaurant-service-discovery-0.0.1-SNAPSHOT.jar service-discovery.jar
ENTRYPOINT ["java", "-jar", "/service-discovery.jar"]
EXPOSE 8761