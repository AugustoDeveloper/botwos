using Newtonsoft.Json;

namespace Botwos.Infrastructure.Integrations.Models.FootballData
{
    public class Standing
    {
        [JsonProperty("stage")]
        public string Stage { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("table")]
        public StandingTable[] Table { get; set; }
    }
}