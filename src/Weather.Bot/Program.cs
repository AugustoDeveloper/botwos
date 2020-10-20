using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace Weather.Bot
{
    class Program
    {
        async static Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("WEATHER_BOT_");
            var configuration = builder.Build();


            var host = CreateHostBuilder(configuration, args)
                .Build();

            var discordBot = host.Services.GetService<DiscordBot>();

            await host.RunAsync();

            discordBot.Dispose();

        }

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(configuration);
                })
                .UseDefaultServiceProvider(options => {
                    options.ValidateScopes = false;
                });
    }
}
