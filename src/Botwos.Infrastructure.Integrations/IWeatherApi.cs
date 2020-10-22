using System.Threading.Tasks;
using Botwos.Infrastructure.Integrations.Models.Weather;

namespace Botwos.Infrastructure.Integrations
{
    public interface IWeatherApi
    {
        Task<WeatherApiResponseModel> GetCurrentWeatherAsync(string uf);
    }
}