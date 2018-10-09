# Summary
munERP (muncinsky Enterprise resource planning) is an experimental project implementing basic ERP system based on microservice architecture. Purpose is to show common concepts to other developers or colegues, but core of the project is mature enough to be used even in the production.

Project is composed of services each representing one Bounded context from DDD's perspective (Accounting, Sales,...) Currently there is only one client application, which is for win desktop and is developed in WPF. Another technologies will be added soon.

# Concepts

# Getting started
Project can be built inside of Docker container. Depending on type of your Docker host use:
 - either [build.dev.docker.linux.bat](build/build.dev.docker.linux.bat)
 - or [build.dev.docker.win.bat](build/build.dev.docker.win.bat)
 
 Have in mind that build running inside of linux container doesn't build windows desktop client.
 
 To run project inside of container use:
 - either [run.dev.docker.linux.bat](build/run.dev.docker.linux.bat)
 - or [run.dev.docker.win.bat](build/run.dev.docker.win.bat)
