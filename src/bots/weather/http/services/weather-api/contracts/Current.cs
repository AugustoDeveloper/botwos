using System.Text.Json.Serialization;

namespace Botwos.Bots.Weather.Http.Services.WeatherAPI.Contracts;

public record struct Current
{
    [JsonPropertyName("last_updated_epoch")]
    public int? LastUpdatedEpoch { get; init; }

    [JsonPropertyName("last_updated")]
    public string? LastUpdated { get; init; }

    [JsonPropertyName("temp_c")]
    public double? TempC { get; init; }

    [JsonPropertyName("temp_f")]
    public double? TempF { get; init; }

    [JsonPropertyName("is_day")]
    public int? IsDay { get; init; }

    [JsonPropertyName("condition")]
    public Condition? Condition { get; init; }

    [JsonPropertyName("wind_mph")]
    public double? WindMph { get; init; }

    [JsonPropertyName("wind_kph")]
    public double? WindKph { get; init; }

    [JsonPropertyName("wind_degree")]
    public int? WindDegree { get; init; }

    [JsonPropertyName("wind_dir")]
    public string? WindDir { get; init; }

    [JsonPropertyName("pressure_mb")]
    public double? PressureMb { get; init; }

    [JsonPropertyName("pressure_in")]
    public double? PressureIn { get; init; }

    [JsonPropertyName("precip_mm")]
    public double? PrecipMm { get; init; }

    [JsonPropertyName("precip_in")]
    public double? PrecipIn { get; init; }

    [JsonPropertyName("humidity")]
    public int? Humidity { get; init; }

    [JsonPropertyName("cloud")]
    public int? Cloud { get; init; }

    [JsonPropertyName("feelslike_c")]
    public double? FeelslikeC { get; init; }

    [JsonPropertyName("feelslike_f")]
    public double? FeelslikeF { get; init; }

    [JsonPropertyName("vis_km")]
    public double? VisKm { get; init; }

    [JsonPropertyName("vis_miles")]
    public double? VisMiles { get; init; }

    [JsonPropertyName("uv")]
    public double? Uv { get; init; }

    [JsonPropertyName("gust_mph")]
    public double? GustMph { get; init; }

    [JsonPropertyName("gust_kph")]
    public double? GustKph { get; init; }

}
