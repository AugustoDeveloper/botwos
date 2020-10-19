using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using Weather.Bot.Modules;
using Weather.Bot.Integrations.Extensions;
using Weather.Bot.Configurations;
using Weather.Bot.Integrations.Configurations;

namespace Weather.Bot
{
    class Program
    {
        static private DiscordSocketClient client;
        static private CommandService commandService;
        static private ServiceProvider provider;

        async static Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("WEATHER_BOT_");
            var configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddWeatherApiClient(svc =>
            {
                var weatherConfiguration = configuration.GetSection("API").Get<WeatherApiConfiguration>();
                svc.AddSingleton<IWeatherApiConfiguration>(weatherConfiguration);
            });

            provider = services.BuildServiceProvider();
            var token = configuration.GetValue("Discord:Token", string.Empty);

            try
            {
                client = new DiscordSocketClient();

                commandService = new CommandService(new CommandServiceConfig
                {
                    CaseSensitiveCommands = false,
                });

                await commandService.AddModuleAsync<WeatherModule>(provider);

                client.Log += LogAsync;
                client.MessageReceived += PerformMessageReceivedHandler;
                
                await client.LoginAsync(TokenType.Bot, token ?? throw new ArgumentNullException(token));
                await client.StartAsync();

                await Task.Delay(-1);
            }
            finally
            {
                await client.LogoutAsync();
                await client.StopAsync();
            }
        }

        async private static Task PerformMessageReceivedHandler(SocketMessage arg)
        {
            // Bail out if it's a System Message.
            var msg = arg as SocketUserMessage;
            if (msg == null) return;

            // We don't want the bot to respond to itself or other bots.
            if (msg.Author.Id == client.CurrentUser.Id || msg.Author.IsBot) return;

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            // Replace the '!' with whatever character
            // you want to prefix your commands with.
            // Uncomment the second half if you also want
            // commands to be invoked by mentioning the bot instead.
            if (msg.HasCharPrefix('!', ref pos) /* || msg.HasMentionPrefix(_client.CurrentUser, ref pos) */)
            {
                // Create a Command Context.
                var context = new SocketCommandContext(client, msg);
                var command = await commandService.GetExecutableCommandsAsync(context, provider);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully).
                var result = await commandService.ExecuteAsync(context, pos, provider);

                // Uncomment the following lines if you want the bot
                // to send a message if it failed.
                // This does not catch errors from commands with 'RunMode.Async',
                // subscribe a handler for '_commands.CommandExecuted' to see those.
                //if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                //    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        static private Task LogAsync(LogMessage entry)
        {
            Console.WriteLine(entry);
            return Task.CompletedTask;
        }
    }
}
