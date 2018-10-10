docker-compose -f yml/docker-compose.yml -f yml/docker-compose.win.yml -p munerp up -d --remove-orphans --force-recreate --build || (pause && exit)
docker ps
docker-compose -f yml/docker-compose.yml -p munerp --no-ansi logs -f -t
pause

