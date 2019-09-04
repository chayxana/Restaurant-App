FROM openjdk:8-jdk-alpine

RUN apk add --update curl git jq python py-pip
RUN pip --cache-dir=pip_cache install awscli