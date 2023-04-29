using System.Net.Http.Json;
using System.Text.Json;
using Botwos.Bots.Weather.Http.Services.WeatherAPI.Abstractions;
using Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI;

public class WeatherAPIService : IWeatherAPIService
{
    private readonly HttpClient client;
    private readonly WeatherAPIKey key;
    private static Dictionary<string, string> StatesAndCapitalFromBrazil;

    static WeatherAPIService()
    {
        StatesAndCapitalFromBrazil = new()
        {
            {"AC", "Rio Branco"},
            {"AL", "Maceio"},
            {"AP", "Macapa"},
            {"AM", "Manaus"},
            {"BA", "Salvador"},
            {"CE", "Fortaleza"},
            {"DF", "Brasilia"},
            {"ES", "Vitoria"},
            {"GO", "Goiania"},
            {"MA", "Sao Luis"},
            {"MT", "Cuiaba"},
            {"MS", "Campo Grande"},
            {"MG", "Belo Horizonte"},
            {"PA", "Belem"},
            {"PB", "Joao Pessoa"},
            {"PR", "Curitiba"},
            {"PE", "Recife"},
            {"PI", "Teresina"},
            {"RJ", "Rio de Janeiro"},
            {"RN", "Natal"},
            {"RS", "Porto Alegre"},
            {"RO", "Porto Velho"},
            {"RR", "Boa Vista"},
            {"SC", "Florianopolis "},
            {"SP", "São Paulo"},
            {"SE", "Aracaju"},
            {"TO", "Palmas"},
        };
    }


    public WeatherAPIService(WeatherAPIKey key, HttpClient client)
    {
        this.key = key;
        this.client = client;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string stateOrCity)
    {
        var response = await this.client.GetFromJsonAsync<WeatherResponse>($"current.json?key={key}&q={ToCapitalIfExists(stateOrCity)}");

        return response;
    }

    private static string ToCapitalIfExists(string stateOrCity)
    {
        var value = stateOrCity?.Trim().ToUpper();
        if (!string.IsNullOrWhiteSpace(value) && StatesAndCapitalFromBrazil.ContainsKey(value))
        {
            return StatesAndCapitalFromBrazil[value];
        }

        return stateOrCity ?? string.Empty;
    }

    public record WeatherAPIKey(string? Key)
    {
        public override string? ToString()
        {
            return Key;
        }
    }
}
