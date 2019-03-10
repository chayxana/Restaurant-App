FROM java:alpine AS builder

COPY . /usr/src/ordering_api
WORKDIR /usr/src/ordering_api

RUN ./gradlew build -x test

FROM java:alpine
WORKDIR /root/
COPY --from=builder /usr/src/ordering_api/build/libs/order.api.jar .
COPY --from=builder /usr/src/ordering_api/build/libs/order.api-tests.jar .

EXPOSE 8090
CMD ["java", "-jar", "./order.api.jar"]