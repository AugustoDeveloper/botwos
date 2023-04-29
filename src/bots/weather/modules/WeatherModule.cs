using Botwos.Bots.Weather.Http.Services.SupaBase.Abstractions;
using Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;
using Botwos.Bots.Weather.Http.Services.WeatherAPI.Abstractions;
using Discord.Commands;

namespace Botwos.Bots.Weather.Modules;

public class WeatherModule : ModuleBase<SocketCommandContext>
{
    private readonly ISupabaseService databaseService;
    private readonly IWeatherAPIService weatherService;

    private int RandomShortCode => Random.Shared.Next(0, 9);

    public WeatherModule(ISupabaseService databaseService, IWeatherAPIService weatherService)
    {
        this.databaseService = databaseService;
        this.weatherService = weatherService;
    }

    protected override async Task BeforeExecuteAsync(CommandInfo command)
    {
        await base.BeforeExecuteAsync(command);
    }

    [Command("weather", true), Summary("Get a weather from Brasil's state\\city")]
    public Task HandleWeatherCommandAsync([Summary("State\\City from Brasil")] params string[] stateOrCity)
        => GetWeatherFromAsync(stateOrCity.Aggregate((cityA, cityB) => $"{cityA} {cityB}"), "en-US");

    [Command("clima", true), Summary("Busca a temperatura em um estado\\cidade")]
    public Task HandleClimaCommandAsync([Summary("Estado\\Cidade do Brasil")] params string[] stateOrCity)
        => GetWeatherFromAsync(stateOrCity.Aggregate((cityA, cityB) => $"{cityA} {cityB}"), "pt-BR");

    private async Task GetWeatherFromAsync(string stateOrCity, string lang)
    {
        try
        {
            var weatherResponse = await weatherService.GetCurrentWeatherAsync(stateOrCity);

            var greeting = await databaseService.GetGreetingAsync(RandomShortCode);

            var shotCode = RandomShortCode;
            var initialPhrase = await databaseService.GetInitialPhraseAsync(shotCode, weatherResponse.Value.Current.Value.FeelslikeC.Value);
            var finalPhrase = await databaseService.GetFinalPhraseAsync(RandomShortCode, weatherResponse.Value.Current.Value.PrecipMm.Value, weatherResponse.Value.Current.Value.Cloud.Value);

            var format = GetFormatPhrase(greeting!, initialPhrase!, finalPhrase!);
            var phrase = string.Format(format, this.Context.User.Username, weatherResponse.Value.Current.Value.TempC, weatherResponse.Value.Location.Value.Name);

            await ReplyAsync(phrase);
        }
        catch (Exception ex)
        {
            await ReplyAsync("Acabou porra...A-CA-BOU...");
            Console.WriteLine(ex);
        }
    }

    private string GetFormatPhrase(Greeting greeting, InitialPhrase initial, FinalPhrase final)
    {
        return $":cityscape: {{2}}\n{greeting?.TextFormat ?? "Hello {0}, "}{initial?.TextFormat ?? "it is {1}°C"}";
    }
}
