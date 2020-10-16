using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.Bot.Integrations
{
    interface IWeatherApi
    {
        WeatherApiResponseModel GetCurrentWeather(string token, string uf);
    }
}
