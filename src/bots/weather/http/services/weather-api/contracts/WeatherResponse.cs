namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

public record struct WeatherResponse(Location? Location, Current? Current);
