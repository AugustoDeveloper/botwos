using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Botwos.Infrastructure.Integrations.Extensions;
using Botwos.Infrastructure.Bot.Extensions;
using System.IO;
using Microsoft.AspNetCore.Http;
using Botwos.SerieA.Bot.Configurations;
using Botwos.Infrastructure.Integrations.Configurations;

namespace Botwos.SerieA.Bot
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
                .AddFootballDataApi(svc => 
                {
                    var configuration = Configuration.GetSection("API").Get<FootballDataApiConfiguration>();
                    svc.AddSingleton<IFootballDataApiConfiguration>(configuration);
                })
                .AddDiscordBot<SerieABot>(Configuration["Discord:Token"]);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var logger = loggerFactory.CreateLogger("webhook");

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/webhook", inApp => 
            {
                inApp.Run(async context => 
                {
                    using var streamReader = new StreamReader(context.Request.Body);
                    var body = await streamReader.ReadToEndAsync();
                    logger.LogInformation($"{body}");

                    context.Response.ContentType = context.Request.ContentType;
                    context.Response.ContentLength = context.Request.ContentLength;
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    var bytes = System.Text.Encoding.UTF8.GetBytes(body);

                    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                });
            });
        }
    }
}