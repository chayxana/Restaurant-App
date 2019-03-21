FROM openjdk:8-jdk-alpine

ADD build/libs/order.api.jar order.api.jar

EXPOSE 8090
CMD ["java", "-jar", "./order.api.jar"]