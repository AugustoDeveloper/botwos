using Newtonsoft.Json;

namespace Botwos.Infrastructure.Integrations.Models.FootballData
{
    public class StandingAggregateResponse
    {
        [JsonProperty("filters")]
        public object Filters { get; set; }
        
        [JsonProperty("competition")]
        public object Competition { get; set; }

        [JsonProperty("season")]
        public object Season { get; set; }
        
        [JsonProperty("standings")]
        public Standing[] Standings { get; set; }
    }
}