using System;
using Microsoft.Extensions.DependencyInjection;
using Botwos.Infrastructure.Integrations.Configurations;

namespace Botwos.Infrastructure.Integrations.Extensions
{
    static public class ServiceCollectionExtension
    {
        static public IServiceCollection AddFootballDataApi(this IServiceCollection services, Action<IServiceCollection> configureFootballDataApiConfiguration)
        {
            services.AddHttpClient<IFootballDataApi, FootballDataApi>((svp, client) =>
            {
                var configuration = svp.GetRequiredService<IFootballDataApiConfiguration>();
                client.BaseAddress = new Uri(configuration.BaseUri);
                client.Timeout = TimeSpan.Parse(configuration.Timeout);
                client.DefaultRequestHeaders.TryAddWithoutValidation("X-Auth-Token", configuration.Token ?? throw new InvalidOperationException("The token to request on football-data is invalid."));
            });

            configureFootballDataApiConfiguration(services);
            return services;
        }
        static public IServiceCollection AddWeatherApiClient(this IServiceCollection services, Action<IServiceCollection> configureWeatherApiConfiguration)
        {
            services.AddHttpClient<IWeatherApi, WeatherApi>((svc, client) => 
            {
                var configuration = svc.GetRequiredService<IWeatherApiConfiguration>();
                client.BaseAddress = new Uri(configuration.BaseUri);
                client.Timeout = TimeSpan.Parse(configuration.Timeout);
            });

            configureWeatherApiConfiguration(services);

            return services;
        }       
    }
}