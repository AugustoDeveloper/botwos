using Botwos.Bots.Weather.Http.Services.WeatherAPI.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddWeatherAPIService(this IServiceCollection services, string? url, string? weatherAPIKey)
    {
        ArgumentException.ThrowIfNullOrEmpty(url);
        ArgumentException.ThrowIfNullOrEmpty(weatherAPIKey);

        services.AddSingleton<WeatherAPIService.WeatherAPIKey>(_ => new(weatherAPIKey));
        services.AddHttpClient<IWeatherAPIService, WeatherAPIService>(h => h.BaseAddress = new(url));
        return services;
    }
}
