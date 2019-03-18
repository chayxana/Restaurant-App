FROM java:alpine

ADD build/libs/restaurant-service-discovery-0.0.1-SNAPSHOT.jar service-discovery.jar
ENTRYPOINT ["java", "-jar", "/service-discovery.jar"]
EXPOSE 8761