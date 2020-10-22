using System.Threading.Tasks;
using Discord.Commands;
using Botwos.Infrastructure.Integrations;
using Botwos.Infrastructure.Integrations.Exceptions.Weather;

namespace Botwos.Weather.Bot.Modules
{
    [Group("weather")]
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        private readonly IWeatherApi api;
        public WeatherModule(IWeatherApi api)
        {
            this.api = api;
        }

        [Command("")]
        [Summary("Get a weather on Brasil's state")]
        async public Task GetWeatherFromAsync([Summary("State from Brasil")] string state)
        {
            try 
            {
                var response = await api.GetCurrentWeatherAsync(state);
                await Context.Channel.SendMessageAsync($"It is {response.Current.TempC}°C in {response.Location.Name}");
            }
            catch(StateNotFoundException ex)
            {
                await Context.Channel.SendMessageAsync(ex.Message);
            }
        }
    }
}
