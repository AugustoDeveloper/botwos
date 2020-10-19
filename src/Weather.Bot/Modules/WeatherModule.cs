using System.Threading.Tasks;
using Discord.Commands;
using Weather.Bot.Integrations;
using Weather.Bot.Integrations.Exceptions;

namespace Weather.Bot.Modules
{
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        private readonly IWeatherApi api;
        public WeatherModule(IWeatherApi api)
        {
            this.api = api;
        }

        [Command("weather")]
        [Summary("Get a weather on Brasil's state")]
        async public Task GetWeatherFromAsync([Summary("State from Brasil")] string state)
        {
            try 
            {
                var response = await api.GetCurrentWeatherAsync(state);
                await Context.Channel.SendMessageAsync($"It is {response.Current.TempC}°C at {response.Location.Name}");
            }
            catch(StateNotFoundException ex)
            {
                await Context.Channel.SendMessageAsync(ex.Message);
            }
        }
    }
}
