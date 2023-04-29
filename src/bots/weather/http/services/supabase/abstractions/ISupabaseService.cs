using Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Abstractions;

public interface ISupabaseService
{
    Task<Greeting?> GetGreetingAsync(int shortCode, CancellationToken cancellation = default);
    Task<InitialPhrase?> GetInitialPhraseAsync(int shortCode, double feelsLikeInCelsius, CancellationToken cancellation = default);
    Task<FinalPhrase?> GetFinalPhraseAsync(int shortCode, double precipitationMM, double cloud, CancellationToken cancellation = default);
}
