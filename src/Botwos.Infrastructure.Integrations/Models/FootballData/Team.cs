using Newtonsoft.Json;

namespace Botwos.Infrastructure.Integrations.Models.FootballData
{
    public class Team
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("crestUrl")]
        public string Logo { get; set; }
    }
}