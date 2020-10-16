using System.Threading.Tasks;
using Discord.Commands;

namespace Weather.Bot.Modules
{
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        [Command("weather")]
        [Summary("Get a weather on Brasil's state")]
        async public Task GetWeatherFromAsync([Summary("State from Brasil")] string state)
        {
            //TODO: Add http integration call to Weather API
            await Context.Channel.SendMessageAsync($"Hello {state}");
        }
    }
}
