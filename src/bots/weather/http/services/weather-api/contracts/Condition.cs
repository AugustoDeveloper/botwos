using System.Text.Json.Serialization;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

public record struct Condition
{
    [JsonPropertyName("text")]
    public string? Text { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("code")]
    public int? Code { get; init; }
}
