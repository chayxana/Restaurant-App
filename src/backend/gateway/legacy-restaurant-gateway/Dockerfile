FROM openjdk:8-jdk-alpine

ADD build/libs/restaurant-gateway-0.0.1.jar gateway.jar
ENTRYPOINT ["java", "-jar", "/gateway.jar"]
EXPOSE 8080