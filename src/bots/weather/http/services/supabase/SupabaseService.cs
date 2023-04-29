using System.Linq;
using System.Linq.Expressions;
using Botwos.Bots.Weather.Http.Services.SupaBase.Abstractions;
using Botwos.Bots.Weather.Http.Services.SupaBase.Contracts;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Supabase.Realtime;
using Supabase.Storage;

namespace Botwos.Bots.Weather.Http.Services.SupaBase;

public class SupabaseService : ISupabaseService
{
    private readonly ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject> client;
    private ISupabaseTable<Greeting, RealtimeChannel> Greetings => client.From<Greeting>();
    private ISupabaseTable<InitialPhrase, RealtimeChannel> InitialPhrases => client.From<InitialPhrase>();
    private ISupabaseTable<FinalPhrase, RealtimeChannel> FinalPhrases => client.From<FinalPhrase>();

    public SupabaseService(ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject> client)
    {
        this.client = client;
    }

    public Task<InitialPhrase?> GetInitialPhraseAsync(int shortCode, double feelsLikeInCelsius, CancellationToken cancellation = default)
        => GetSinglePhraseAsync<InitialPhrase>(
                InitialPhrases,
                i => i.ShortCode.HasValue && i.ShortCode.Value == shortCode &&
                i.InitialFeelsLikeInCelsius <= feelsLikeInCelsius &&
                i.FinalFeelsLikeInCelsius >= feelsLikeInCelsius,
                cancellation);

    public Task<FinalPhrase?> GetFinalPhraseAsync(int shortCode, double precipitationMM, double cloud, CancellationToken cancellation = default)
        => GetSinglePhraseAsync<FinalPhrase>(
                FinalPhrases,
                f => f.ShortCode.HasValue && f.ShortCode.Value == shortCode &&
                f.InitialPrecipitationMM <= precipitationMM &&
                f.FinalPrecipitationMM >= precipitationMM &&
                f.InitialCloudPercentage <= cloud &&
                f.FinalCloudPercentage >= cloud,
                cancellation);

    public Task<Greeting?> GetGreetingAsync(int shortCode, CancellationToken cancellation = default)
        => GetSinglePhraseAsync<Greeting>(
                Greetings,
                g => g.ShortCode.HasValue && g.ShortCode.Value == shortCode,
                cancellation);

    public async Task<TFormatedModel?> GetSinglePhraseAsync<TFormatedModel>(
            ISupabaseTable<TFormatedModel, RealtimeChannel> table,
            Expression<Func<TFormatedModel, bool>> expression,
            CancellationToken cancellation = default)
        where TFormatedModel : FormatedPhraseBase, new()
    {
        var values = await table.Get();
        var phrase = values.Models.FirstOrDefault(expression.Compile());

        return phrase;
    }
}
