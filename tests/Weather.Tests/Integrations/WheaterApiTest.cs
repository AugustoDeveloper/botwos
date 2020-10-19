using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Weather.Bot.Integrations;
using Weather.Bot.Integrations.Configurations;
using Weather.Bot.Integrations.Exceptions;
using Xunit;

namespace Weather.Tests.Integrations
{
    public class WheaterApiTest
    {
        [Fact]
        public void GiveAWeatherApiConstructorReceivingANullArgsShouldThrownArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new WeatherApi(null, null));
            Assert.Throws<ArgumentNullException>(() => new WeatherApi(new HttpClient(), null));
            Assert.Throws<ArgumentNullException>(() => new WeatherApi(null, new Mock<IWeatherApiConfiguration>().Object));
        }

        [Theory, InlineData(null), InlineData(""), InlineData(" "), InlineData("ajnsdçjas"), InlineData("AZ")]
        async public void GivenAInvalidUFShouldReturnStateNotFoundException(string invalidUF)
        {
            await Assert.ThrowsAsync<StateNotFoundException>( () =>
                new WeatherApi(new HttpClient(){ BaseAddress = new Uri("http://api.weatherapi.com/v1/") }, new Mock<IWeatherApiConfiguration>().Object)
                    .GetCurrentWeatherAsync(invalidUF)
            );
        }

        [Theory, InlineData(400), InlineData(401), InlineData(404)]
        async public void GivenAValidUFAndStatusCodeFromAPIIsNotSuccessShouldThrownHttpRequestException(int invalidStatusCode)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = (HttpStatusCode)invalidStatusCode,
                })
                .Verifiable();


            var api = new WeatherApi(new HttpClient(handlerMock.Object) { BaseAddress = new Uri("http://api.weatherapi.com/v1/") }, new Mock<IWeatherApiConfiguration>().Object);

            await Assert.ThrowsAsync<HttpRequestException>(() => api.GetCurrentWeatherAsync("AM"));
        }

        [Theory, InlineData(""), InlineData(" ")]
        async public void GivenAValidUFAndResponseBodyIsInvalidShouldThrownInvalidOperationException(string invalidBody)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(invalidBody),
                })
                .Verifiable();

            var api = new WeatherApi(new HttpClient(handlerMock.Object){ BaseAddress = new Uri("http://api.weatherapi.com/v1/") }, new Mock<IWeatherApiConfiguration>().Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() => api.GetCurrentWeatherAsync("AM"));
        }

        [Fact]
        async public void GivenAValidUFShouldReturnsResponseWeatherApiModel()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent($"{{ \"current\": {{ \"temp_c\": 25.0 }}  }}"),
                })
                .Verifiable();

            var api = new WeatherApi(new HttpClient(handlerMock.Object){ BaseAddress = new Uri("http://api.weatherapi.com/v1/") }, new Mock<IWeatherApiConfiguration>().Object);

            var response = await api.GetCurrentWeatherAsync("AM");
            Assert.Equal(25.0, response.Current.TempC);
        }
    }
}