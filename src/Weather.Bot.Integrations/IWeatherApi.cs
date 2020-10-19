using System.Threading.Tasks;
using Weather.Bot.Integrations.Models;

namespace Weather.Bot.Integrations
{
    public interface IWeatherApi
    {
        Task<WeatherApiResponseModel> GetCurrentWeatherAsync(string uf);
    }
}