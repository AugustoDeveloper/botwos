using System;
using Weather.Bot.Integrations;
using Xunit;

namespace Weather.Tests
{
    public class WheaterApiTest
    {
        private readonly IWeatherApi _WeatherApi;

        public WheaterApiTest()
        {
            _WeatherApi = new WeatherApi();
        }
        [Fact]
        public void GivenAValidTokenAndUFShouldReturnSpecifiCapital()
        {
            var response = _WeatherApi.GetCurrentWeather("2e626cae8c03409289d11817201610", "AM");

            Assert.True(response.Location.Name == "Manaus");
        }

        [Fact]
        public void GivenAValidTokenAndInvalidUFShouldReturnAnException()
        {
            var message = "Digita a uf direito co2";
            var exception = Record.Exception(() => _WeatherApi.GetCurrentWeather("2e626cae8c03409289d11817201610", "XP"));
            Assert.NotNull(exception);
            Assert.True(exception.Message == message);
        }

    }
}
