FROM gradle:6.3-jdk11 as builder

COPY . /usr/src/ordering_api
USER root
RUN chown -R gradle:gradle /usr/src/ordering_api
USER gradle

WORKDIR /usr/src/ordering_api
RUN gradle build

FROM adoptopenjdk/openjdk11-openj9:alpine-jre
WORKDIR /root/
COPY --from=builder /usr/src/ordering_api/build/libs/order.api-*.jar ./order-api.jar

EXPOSE 8090
CMD java $JAVA_OPTS -Xshareclasses -Xquickstart -jar ./order-api.jar
