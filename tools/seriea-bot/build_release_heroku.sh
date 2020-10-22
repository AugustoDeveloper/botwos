docker build . -f src/Botwos.SerieA.Bot/Dockerfile -t seriea-bot
docker tag seriea-bot registry.heroku.com/seriea-bot-net/web
docker push registry.heroku.com/seriea-bot-net/web
heroku container:release web -a seriea-bot-net