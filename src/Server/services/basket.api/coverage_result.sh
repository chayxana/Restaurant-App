#!/bin/sh

OUTPUT_FILE=$1

TOTAL_COVERAGE=$(awk '{if ($1 != "?") print $5; else print "0.0";}' $OUTPUT_FILE | sed 's/\%//g' | awk '{s+=$1} END {printf "%.2f\n", s}') 
TOTAL_LINES=$(awk 'END{ print NR }' $OUTPUT_FILE)

echo $(expr ${TOTAL_COVERAGE%.*} / $TOTAL_LINES)