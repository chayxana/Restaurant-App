FROM adoptopenjdk/openjdk11-openj9:alpine-jre

ARG sourceFilePath=./build/libs/order.api-*.jar

COPY ${sourceFilePath} order.api.jar

EXPOSE 8090
CMD java $JAVA_OPTS -Xshareclasses -Xquickstart -jar ./order.api.jar