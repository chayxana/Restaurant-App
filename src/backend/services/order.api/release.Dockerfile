FROM adoptopenjdk/openjdk11-openj9:alpine-jre
COPY ./order.api.jar order.api.jar

EXPOSE 8090
CMD java $JAVA_OPTS -Xshareclasses -Xquickstart -jar ./order.api.jar