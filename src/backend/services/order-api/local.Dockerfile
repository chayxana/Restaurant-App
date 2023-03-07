FROM maven:3.8.3-openjdk-17-slim AS build

WORKDIR /app

COPY pom.xml .
RUN mvn dependency:go-offline

COPY src src

RUN mvn package -Dquarkus.package.type=uber-jar -DskipTests -Dquarkus.profile=local

## Stage 2 : create the docker final image
FROM eclipse-temurin:17
WORKDIR /work/
COPY --from=build /app/target/*-runner.jar /work/application

EXPOSE 8080
CMD ["java","-Dquarkus.http.host=0.0.0.0", "-Djava.util.logging.manager=org.jboss.logmanager.LogManager", "-jar", "./application"]