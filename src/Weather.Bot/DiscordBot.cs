using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Weather.Bot.Modules;

namespace Weather.Bot
{
    public class DiscordBot : IDisposable
    {
        private bool disposed;
        private readonly IConfiguration configuration;
        private readonly IServiceProvider provider;
        private readonly ILogger logger;
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;
        private CancellationTokenSource cancellationTokenSource;
        private readonly string DiscordToken;
        private Task communitationTask;

        public DiscordBot(IConfiguration configuration, IServiceProvider provider, ILogger<DiscordBot> logger)
        {
            cancellationTokenSource = new CancellationTokenSource();
            this.configuration = configuration;
            this.provider = provider;
            this.logger = logger;
            this.DiscordToken = configuration["Discord:Token"] ?? throw new ArgumentNullException(nameof(DiscordToken));
            this.client = new DiscordSocketClient();
            this.commandService = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
            });

            client.Log += PerformLogAsync;
            client.MessageReceived += PerformMessageReceivedHandlerAsync;

            this.communitationTask = ExecuteCommunicationAsync(cancellationTokenSource.Token);
        }


        async private Task ExecuteCommunicationAsync(CancellationToken token)
        {
            try
            {
                await commandService.AddModuleAsync<WeatherModule>(provider);
                await client.LoginAsync(TokenType.Bot, DiscordToken);
                await client.StartAsync();
                await Task.Delay(-1, token);
            }
            catch(TaskCanceledException){}
            finally
            {
                await client.LogoutAsync();
                await client.StopAsync();
            }
        }

        async private Task PerformMessageReceivedHandlerAsync(SocketMessage arg)
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

        private Task PerformLogAsync(LogMessage entry)
        {
            logger.Log((LogLevel)entry.Severity, entry.Message);
            return Task.CompletedTask;
        }

        virtual protected void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                cancellationTokenSource.Cancel();
                communitationTask.Wait();                

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}