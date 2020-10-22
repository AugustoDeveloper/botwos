using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Botwos.Infrastructure.Bot
{
    abstract public class DiscordBotBase : IDiscordBot
    {
        private bool disposed;
        protected CommandService CommandService { get; }
        protected IServiceProvider ServiceProvider { get; }
        private readonly ILogger logger;
        private readonly DiscordSocketClient client;
        private CancellationTokenSource cancellationTokenSource;
        private readonly string DiscordToken;
        private Task communitationTask;

        virtual protected char PrefixCharacter => '!';

        public DiscordBotBase(string discordToken, IServiceProvider provider, ILogger<IDiscordBot> logger)
        {
            cancellationTokenSource = new CancellationTokenSource();
            this.ServiceProvider = provider;
            this.logger = logger;
            this.DiscordToken = discordToken ?? throw new ArgumentNullException(nameof(DiscordToken));
            this.client = new DiscordSocketClient();
            this.CommandService = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
            });

            client.Log += PerformLogAsync;
            client.MessageReceived += PerformMessageReceivedHandlerAsync;

            this.communitationTask = StartCommunicationAsync(cancellationTokenSource.Token);
        }

        abstract protected Task ConfigureModulesAsync();

        async virtual protected Task StartCommunicationAsync(CancellationToken token)
        {
            try
            {
                await ConfigureModulesAsync();
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
            if (msg.HasCharPrefix(this.PrefixCharacter, ref pos) /* || msg.HasMentionPrefix(_client.CurrentUser, ref pos) */)
            {
                // Create a Command Context.
                var context = new SocketCommandContext(client, msg);
                var command = await CommandService.GetExecutableCommandsAsync(context, ServiceProvider);

                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully).
                var result = await CommandService.ExecuteAsync(context, pos, ServiceProvider);

                // Uncomment the following lines if you want the bot
                // to send a message if it failed.
                // This does not catch errors from commands with 'RunMode.Async',
                // subscribe a handler for '_commands.CommandExecuted' to see those.
                //if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                //    await msg.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        virtual protected Task PerformLogAsync(LogMessage entry)
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