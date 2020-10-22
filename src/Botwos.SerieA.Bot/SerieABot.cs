using System;
using System.Threading.Tasks;
using Botwos.Infrastructure.Bot;
using Botwos.SerieA.Bot.Modules;
using Microsoft.Extensions.Logging;

namespace Botwos.SerieA.Bot
{
    public class SerieABot : DiscordBotBase
    {
        public SerieABot(string discordToken, IServiceProvider provider, ILogger<IDiscordBot> logger) : base(discordToken, provider, logger)
        {
        }

        protected override Task ConfigureModulesAsync()
            => this.CommandService.AddModuleAsync<SerieAModule>(this.ServiceProvider);
        
    }
}