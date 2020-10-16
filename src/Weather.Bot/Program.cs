using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;

namespace Weather.Bot
{
    class Program
    {
        static private DiscordSocketClient client;

        async static Task Main(string[] args)
        {
            try
            {
                client = new DiscordSocketClient();

                client.Log += LogAsync;
                var token = "";
                await client.LoginAsync(TokenType.Bot, token);
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

        static private Task LogAsync(LogMessage entry)
        {
            Console.WriteLine(entry);
            return Task.CompletedTask;
        }
    }
}
