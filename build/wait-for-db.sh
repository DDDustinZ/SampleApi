#!/bin/bash
until sqlcmd -S "$1","$2" -U "$3" -P "$4" -Q " "
do
    sleep 1
done