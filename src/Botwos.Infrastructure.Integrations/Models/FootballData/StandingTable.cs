using Newtonsoft.Json;

namespace Botwos.Infrastructure.Integrations.Models.FootballData
{
    public class StandingTable
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("team")]
        public Team Team { get ;set; }

        [JsonProperty("points")]
        public int Points { get; set; }
    }
}