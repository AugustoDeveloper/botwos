using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weather.Bot.Integrations
{
    public sealed class WeatherApi : IWeatherApi
    {
        public WeatherApiResponseModel GetCurrentWeather(string token, string uf)
        {
            var client = new RestClient("http://api.weatherapi.com/v1");

            var city = Helper.UfToState().Where(u => u.UF == uf.ToUpper()).Select(u => u.Name).FirstOrDefault();

            if (string.IsNullOrEmpty(city))
                throw new Exception("Digita a uf direito co2");

            var request = new RestRequest("current.json?key="+token+"&q="+city, DataFormat.Json);

            IRestResponse<WeatherApiResponseModel> response = client.Get<WeatherApiResponseModel>(request);

            return response.Data;
        }
    }
}
