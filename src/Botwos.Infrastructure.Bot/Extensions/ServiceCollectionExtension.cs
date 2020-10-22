using System;
using Botwos.Infrastructure.Bot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Botwos.Infrastructure.Bot.Extensions
{
    static public class ServiceCollectionExtension
    {
        static public IServiceCollection AddDiscordBot<TDiscordBot>(this IServiceCollection services, string discordToken)
        where TDiscordBot : IDiscordBot
        => services
            .AddSingleton(typeof(TDiscordBot), svc => 
                (TDiscordBot)Activator.CreateInstance(typeof(TDiscordBot), 
                discordToken, 
                svc, 
                svc.GetRequiredService<ILogger<IDiscordBot>>()));
    }
}