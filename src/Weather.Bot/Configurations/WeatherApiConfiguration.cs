using Weather.Bot.Integrations.Configurations;

namespace Weather.Bot.Configurations
{
    public class WeatherApiConfiguration : IWeatherApiConfiguration
    {
        public string BaseUri { get; set; }

        public string Timeout { get; set; }

        public string Key { get; set; }
    }
}