using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.Bot.Integrations.Extensions;
using Microsoft.Extensions.Logging;
using Weather.Bot.Integrations.Configurations;
using Weather.Bot.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Weather.Bot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging(l => l.AddConsole());
            services
                .AddWeatherApiClient(svc =>
                {
                    var weatherConfiguration = Configuration.GetSection("API").Get<WeatherApiConfiguration>();
                    svc.AddSingleton<IWeatherApiConfiguration>(weatherConfiguration);
                })
                .AddSingleton<DiscordBot>(svc => 
                    new DiscordBot(Configuration, svc, svc.GetRequiredService<ILogger<DiscordBot>>())
                );
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/echo", app => 
            { 
                app.Run(context => 
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    return Task.CompletedTask;
                });
            });
        }
    }
}