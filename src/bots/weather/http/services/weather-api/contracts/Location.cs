using System.Text.Json.Serialization;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

public record struct Location
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    [JsonPropertyName("region")]
    public string? Region { get; init; }
    [JsonPropertyName("country")]
    public string? Country { get; init; }
    [JsonPropertyName("lat")]
    public double? Lat { get; init; }
    [JsonPropertyName("lon")]
    public double? Lon { get; init; }
    [JsonPropertyName("tz_id")]
    public string? TzId { get; init; }
    [JsonPropertyName("localtime_epoch")]
    public int? LocaltimeEpoch { get; init; }
    [JsonPropertyName("localtime")]
    public string? Localtime { get; init; }
}
