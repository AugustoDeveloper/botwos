using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Bot.Integrations
{
    public interface IWeatherApi
    {
        WeatherApiResponseModel GetCurrentWeather(string token, string uf);
    }
}
