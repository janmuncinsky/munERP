docker-compose -f yml/docker-compose.win.yml -f yml/docker-compose.override.yml -p mERP up -d --remove-orphans --force-recreate --build || (pause && exit)
docker ps
docker-compose -f yml/docker-compose.win.yml -p mERP --no-ansi logs -f -t
pause

