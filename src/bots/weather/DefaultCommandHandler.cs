using Botwos.Bots.Weather.Modules;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Botwos.Bots.Weather;

internal class DefaultCommandHandler
{
    private readonly DiscordSocketClient client;
    private readonly CommandService commandService;
    private readonly IServiceProvider provider;

    public DefaultCommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService commandService)
        => (this.provider, this.client, this.commandService) = (provider, client, commandService);

    public async Task<DefaultCommandHandler> SetupAsync(string? token)
    {
        client.MessageReceived += HandleMessageByModuleAsync;
        client.Log += HandleLogMessage;

        await commandService.AddModuleAsync<WeatherModule>(provider);

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();

        return this;
    }

    public Task RunAsync() => Task.Delay(-1);

    private Task HandleLogMessage(LogMessage message)
    {
        Console.WriteLine($"[{message.Severity}] - {message.Message}");

        return Task.CompletedTask;
    }

    private async Task HandleMessageByModuleAsync(SocketMessage messageParam)
    {
        try
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;


            if (message.Author.IsBot || !message.HasCharPrefix('!', ref argPos) || client.CurrentUser.Id == message.Author.Id)
            {
                return;
            }

            var context = new SocketCommandContext(client, message);

            await commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: provider);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
