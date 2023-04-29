using Postgrest.Attributes;
using Postgrest.Models;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;

public abstract class FormatedPhraseBase : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; init; }

    [Column("short_code")]
    public int? ShortCode { get; init; }

    [Column("name")]
    public string? Name { get; init; }

    [Column("description")]
    public string? Description { get; init; }

    [Column("text_format")]
    public string? TextFormat { get; init; }
}
