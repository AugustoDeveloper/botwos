using Newtonsoft.Json;
using System;

namespace Botwos.Infrastructure.Integrations.Models.Weather
{
    public class WeatherApiResponseModel
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("current")]
        public Current Current { get; set; }
    }
}