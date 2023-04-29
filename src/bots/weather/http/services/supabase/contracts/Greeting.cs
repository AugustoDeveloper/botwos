using Postgrest.Attributes;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;

[Table("greetings")]
public class Greeting : FormatedPhraseBase
{
    public Greeting() { }
}
