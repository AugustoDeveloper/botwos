using Botwos.Bots.Weather.Http.Services.SupaBase;
using Botwos.Bots.Weather.Http.Services.SupaBase.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Supabase.Realtime;
using Supabase.Storage;

namespace Botwos.Bots.Weather.Http.Services.SupaBase.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddSupabaseService(this IServiceCollection services, string? url, string? key)
    {
        ArgumentException.ThrowIfNullOrEmpty(url);
        ArgumentException.ThrowIfNullOrEmpty(key);

        Func<IServiceProvider, ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject>> inject = (_) =>
        {
            ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject> client = new Supabase.Client(url, key);

            client.InitializeAsync().GetAwaiter().GetResult();

            return client;
        };

        services.AddScoped<ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject>>(inject);
        services.AddScoped<ISupabaseService, SupabaseService>();
        return services;
    }
}

