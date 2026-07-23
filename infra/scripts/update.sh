#!/bin/bash
git fetch origin
git reset --hard origin/main
docker compose -f docker-compose.prod.yml pull
docker compose -f docker-compose.prod.yml up -d
docker exec nginx nginx -t
docker exec nginx nginx -s reload