using Postgrest.Attributes;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;

[Table("final_phrases")]
public class FinalPhrase : FormatedPhraseBase
{
    [Column("initial_precipitation_mm")]
    public double InitialPrecipitationMM { get; set; }

    [Column("final_precipitation_mm")]
    public double FinalPrecipitationMM { get; set; }

    [Column("initia_cloud_percentage")]
    public double InitialCloudPercentage { get; set; }

    [Column("final_cloud_percentage")]
    public double FinalCloudPercentage { get; set; }

    public FinalPhrase() { }
}

