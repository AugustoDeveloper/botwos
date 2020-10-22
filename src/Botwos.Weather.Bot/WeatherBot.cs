using System;
using System.Threading.Tasks;
using Botwos.Infrastructure.Bot;
using Botwos.Weather.Bot.Modules;
using Microsoft.Extensions.Logging;

namespace Botwos.Weather.Bot
{
    public class WeatherBot : DiscordBotBase
    {
        public WeatherBot(string discordToken, IServiceProvider provider, ILogger<IDiscordBot> logger) : base(discordToken, provider, logger)
        {
        }

        protected override Task ConfigureModulesAsync()
            => this.CommandService.AddModuleAsync<WeatherModule>(this.ServiceProvider);
        
    }
}