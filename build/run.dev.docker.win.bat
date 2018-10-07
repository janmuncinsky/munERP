docker-compose -f yml/docker-compose.win.yml -f yml/docker-compose.override.yml -p munERP up -d --remove-orphans --force-recreate --build || (pause && exit)
docker ps
docker-compose -f yml/docker-compose.win.yml -p munERP --no-ansi logs -f -t
pause

