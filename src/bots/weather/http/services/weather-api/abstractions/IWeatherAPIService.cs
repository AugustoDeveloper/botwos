using Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Abstractions;

public interface IWeatherAPIService
{
    Task<WeatherResponse?> GetCurrentWeatherAsync(string stateOrCity);
}

