docker-compose -f yml/docker-compose.linux.yml -f yml/docker-compose.override.yml -p merp up -d --remove-orphans --force-recreate --build || (pause && exit)
docker ps
docker-compose -f yml/docker-compose.linux.yml -p merp --no-ansi logs -f -t
pause

