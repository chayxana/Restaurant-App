#!/usr/bin/env bash

covered=0
total=0
while IFS='' read -r line || [[ -n "$line" ]]; do
    IFS=' ' read -r -a array <<< "$line"
    total=$(($total+${array[1]}))
    if [ "${array[2]}" = "1" ]; then
        covered=$(($covered+${array[1]}))
    fi 
done < "$1"
echo $(awk "BEGIN { pc=100*${covered}/${total}; i=int(pc); print (pc-i<0.5)?i:i+1 }")