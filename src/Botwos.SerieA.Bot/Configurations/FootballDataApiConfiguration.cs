using Botwos.Infrastructure.Integrations.Configurations;

namespace Botwos.SerieA.Bot.Configurations
{
    public class FootballDataApiConfiguration : IFootballDataApiConfiguration
    {
        public string BaseUri { get; set; }

        public string Timeout { get; set; }

        public string Token { get; set; }
    }
}