#!/bin/sh

./gradlew test jacocoTestReport

cat build/reports/jacoco/test/html/index.html