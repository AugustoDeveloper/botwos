using Newtonsoft.Json;
using System;

namespace Weather.Bot.Integrations.Models
{
    public class WeatherApiResponseModel
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("current")]
        public Current Current { get; set; }
    }
}