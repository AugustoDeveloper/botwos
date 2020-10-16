using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Weather.Bot.Modules;

namespace Weather.Bot
{
    class Program
    {
        static private DiscordSocketClient client;
        static private CommandService commandService;

        async static Task Main(string[] args)
        {
            try
            {
                client = new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug
                });

                commandService = new CommandService(new CommandServiceConfig
                {
                    CaseSensitiveCommands = false,
                });
                await commandService.AddModuleAsync<WeatherModule>(null);

                client.Log += LogAsync;
                client.MessageReceived += PerformMessageReceivedHandler;

                var token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
                
                await client.LoginAsync(TokenType.Bot, token ?? throw new ArgumentNullException(token));
                await client.StartAsync();

                Console.ReadKey();
            }
            finally
            {
                await client.LogoutAsync();
                await client.StopAsync();
            }
            Console.ReadKey();
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
                var command = await commandService.GetExecutableCommandsAsync(context, null);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully).
                var result = await commandService.ExecuteAsync(context, pos, null);

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
