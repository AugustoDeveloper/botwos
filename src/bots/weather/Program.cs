using Botwos.Bots.Weather;
using Botwos.Bots.Weather.Http.Services.SupaBase.Extensions;
using Botwos.Bots.Weather.Http.Services.WeatherAPI.Extensions;
using Botwos.Bots.Weather.Modules;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("##############################################");
Console.WriteLine("           Botwos - Weather Bot               ");
Console.WriteLine("##############################################");
var token = Environment.GetEnvironmentVariable("WEATHER_BOT_DISCORD_TOKEN");
var supabaseUrl = Environment.GetEnvironmentVariable("WEATHER_BOT_SUPABASE_URL");
var supabaseKey = Environment.GetEnvironmentVariable("WEATHER_BOT_SUPABASE_KEY");
var weaterApiKey = Environment.GetEnvironmentVariable("WEATHER_BOT_WEATHERAPI_KEY");
var weatherApiUrl = Environment.GetEnvironmentVariable("WEATHER_BOT_WEATHERAPI_URL");

IServiceCollection services = new ServiceCollection();

services.AddSupabaseService(supabaseUrl, supabaseKey);
services.AddWeatherAPIService(weatherApiUrl, weaterApiKey);

services.AddScoped<WeatherModule>();
services.AddSingleton(_ => new DiscordSocketClient(new() { GatewayIntents = Discord.GatewayIntents.All }));
services.AddSingleton(_ => new CommandService());
services.AddSingleton<DefaultCommandHandler>();

using var provider = services.BuildServiceProvider();

var handler = provider.GetRequiredService<DefaultCommandHandler>();

await handler
    .SetupAsync(token);

await handler
    .RunAsync();

