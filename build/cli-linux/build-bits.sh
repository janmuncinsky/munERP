#!/bin/bash
set -e
# echo -e "\e[33m\tClearing output"
# rm -rf --verbose "docker/images/output"
echo -e "\e[33m\tBuilding the solution"
dotnet publish src/MunCode.mERP.sln -c Debug