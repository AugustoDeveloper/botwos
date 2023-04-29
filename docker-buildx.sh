dotnet publish src/bots/weather/ -c Release -o out/arm -r linux-arm -f net7.0

docker buildx build --platform=linux/arm/v7 --no-cache . -f src/bots/weather/Dockerfile.Arm64 -t tavolatech.azurecr.io/weather-bot:1.X-arm32
