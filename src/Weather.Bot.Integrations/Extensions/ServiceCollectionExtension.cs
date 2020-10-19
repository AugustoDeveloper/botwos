using System;
using Microsoft.Extensions.DependencyInjection;
using Weather.Bot.Integrations.Configurations;

namespace Weather.Bot.Integrations.Extensions
{
    static public class ServiceCollectionExtension
    {
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