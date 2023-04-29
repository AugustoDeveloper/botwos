using Postgrest.Attributes;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;

[Table("initial_phrases")]
public class InitialPhrase : FormatedPhraseBase
{
    [Column("initial_feels_like_c")]
    public double InitialFeelsLikeInCelsius { get; init; }

    [Column("final_feels_like_c")]
    public double FinalFeelsLikeInCelsius { get; init; }

    public InitialPhrase() { }
}

