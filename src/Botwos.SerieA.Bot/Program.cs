using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Botwos.SerieA.Bot
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables("SERIEA_BOT_");
            var configuration = builder.Build();
            
            var host = CreateHostBuilder(configuration, args)
                .Build();

            var discordBot = host.Services.GetService<SerieABot>();

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
