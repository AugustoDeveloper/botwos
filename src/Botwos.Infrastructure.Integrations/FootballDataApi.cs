using System;
using System.Net.Http;
using System.Threading.Tasks;
using Botwos.Infrastructure.Integrations.Models.FootballData;
using Newtonsoft.Json;

namespace Botwos.Infrastructure.Integrations
{
    public class FootballDataApi : IFootballDataApi
    {
        private readonly HttpClient client;
        public FootballDataApi(HttpClient client)
        {
            this.client = client;
        }
        async public Task<StandingAggregateResponse> GetStandingAsync()
        {
            var response = await this.client.GetAsync($"competitions/BSA/standings?standingType=TOTAL");
            response.EnsureSuccessStatusCode();

            var responseBodyText = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(responseBodyText))
            {
                throw new InvalidOperationException("The content is invalid to perform this operation.");
            }

            return JsonConvert.DeserializeObject<StandingAggregateResponse>(responseBodyText);
        }
    }
}