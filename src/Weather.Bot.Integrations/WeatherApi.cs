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

            if (!Helper.CapitalsFromBrazil.ContainsKey(uf.ToUpper()))
                throw new Exception("Digita a uf direito co2");

            var state = Helper.CapitalsFromBrazil[uf.ToUpper()];

            var request = new RestRequest("current.json?key="+token+"&q="+state, DataFormat.Json);

            IRestResponse<WeatherApiResponseModel> response = client.Get<WeatherApiResponseModel>(request);

            return response.Data;
        }
    }
}
