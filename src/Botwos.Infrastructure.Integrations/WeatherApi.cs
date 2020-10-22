using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Botwos.Infrastructure.Integrations.Configurations;
using Botwos.Infrastructure.Integrations.Extensions;
using Botwos.Infrastructure.Integrations.Models.Weather;

namespace Botwos.Infrastructure.Integrations
{
    public sealed class WeatherApi : IWeatherApi
    {
        private readonly HttpClient client;
        private readonly IWeatherApiConfiguration configuration;
        public WeatherApi(HttpClient client, IWeatherApiConfiguration configuration)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        async public Task<WeatherApiResponseModel> GetCurrentWeatherAsync(string uf)
        {
            var response = await this.client.GetAsync($"current.json?key={configuration.Key}&q={uf.ToCapital()}");
            response.EnsureSuccessStatusCode();

            var responseBodyText = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBodyText))
            {
                throw new InvalidOperationException("The content is invalid to perform this operation.");
            }

            return JsonConvert.DeserializeObject<WeatherApiResponseModel>(responseBodyText);
        }
    }
}