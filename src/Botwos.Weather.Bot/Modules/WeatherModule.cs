using System.Threading.Tasks;
using Discord.Commands;
using Botwos.Infrastructure.Integrations;
using System.Linq;
using System.Net.Http;
using Botwos.Weather.Infrastructure.Persistence;
using Botwos.Weather.Bot.Core;
using System;

namespace Botwos.Weather.Bot.Modules
{
    public class WeatherModule : ModuleBase<SocketCommandContext>
    {
        private readonly IWeatherApi api;
        private readonly DbResponsesContext dbContext;

        public WeatherModule(IWeatherApi api, DbResponsesContext dbContext)
        {
            this.api = api;
            this.dbContext = dbContext;
        }

        async private Task GetWeatherFromAsync(string stateOrCity, string lang)
        {
            var shortIds = DateTime.Now.GetRandomShortCodeIds();
            try
            {
                var response = await api.GetCurrentWeatherAsync(stateOrCity);
                var responseFormat = await dbContext.GenerateSuccessResponseFormatMessageAsync(lang,
                    response.Current.FeelslikeC,
                    response.Current.PrecipMm,
                    response.Current.Cloud,
                    shortIds[0],
                    shortIds[1],
                    shortIds[2]);

                var responseText = string.Format(responseFormat, this.Context.User.Username, response.Current.TempC, response.Location.Name);

                await Context.Channel.SendMessageAsync(responseText);
            }
            catch(HttpRequestException)
            {
                var responseFailureFormat = await dbContext.GenerateFailureResponseFormatMessage(lang, shortIds[0]);
                var responseFailureText = string.Format(responseFailureFormat, stateOrCity);

                await Context.Channel.SendMessageAsync(responseFailureText);
            }
            catch(Exception ex)
            {
                await Context.Channel.SendMessageAsync("Iiihh...Colou giglê! Oh...Geez Rick!");
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
