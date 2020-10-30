using System.Threading.Tasks;
using Discord.Commands;
using Botwos.Infrastructure.Integrations;
using Botwos.Infrastructure.Integrations.Exceptions.Weather;
using System.Linq;
using System.Net.Http;
using Botwos.Weather.Bot.Resources;
using System.Globalization;

namespace Botwos.Weather.Bot.Modules
{
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        private readonly IWeatherApi api;

        public WeatherModule(IWeatherApi api)
        {
            this.api = api;
        }

        
        async private Task GetWeatherFromAsync(string stateOrCity, string lang)
        {
            var culture = CultureInfo.GetCultureInfo(lang);
            try 
            {
                var format = Messages.ResourceManager.GetString("WeatherResponseFormat", culture);
                var response = await api.GetCurrentWeatherAsync(stateOrCity);
                await Context.Channel.SendMessageAsync(string.Format(format, response.Current.TempC, response.Location.Name, response.Location.Region, response.Location.Country));
            }
            catch(HttpRequestException)
            {
                await Context.Channel.SendMessageAsync(Messages.ResourceManager.GetString("CityOrStateWasNotFound", culture));
            }
        }
        
        [Command("weather", true), Summary("Get a weather on Brasil's state\\city")]
        public Task PerformWeatherCommandAsync([Summary("State\\City from Brasil")] params string[] stateOrCity)
            =>  GetWeatherFromAsync(stateOrCity.Aggregate((cityA, cityB) => $"{cityA} {cityB}"), "en-US");

        [Command("clima", true), Summary("Busca a temperatura em um estado\\cidade")]
        public Task PerformClimaCommandAsync([Summary("Estado\\Cidade do Brasil")] params string[] stateOrCity)
            =>  GetWeatherFromAsync(stateOrCity.Aggregate((cityA, cityB) => $"{cityA} {cityB}"), "pt-BR");
    }
}
