docker build . -f src/Botwos.Weather.Bot/Dockerfile -t weather-bot
docker tag weather-bot registry.heroku.com/weather-bot-net/web
docker push registry.heroku.com/weather-bot-net/web
heroku container:release web -a weather-bot-net