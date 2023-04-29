FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-bullseye-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["./src/bots/weather/", "weather/"]
RUN dotnet publish weather/ -c Release -o /app/publish -r linux-arm64 -f net7.0

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN ls
ENTRYPOINT ["./Botwos.Bots.Weather"]

