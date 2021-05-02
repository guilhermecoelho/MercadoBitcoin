using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using MercadoBitcoin.Service.Entities;
using MercadoBitcoin.Test.Builders;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class TickerServiceTest
    {
        private Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private IConfiguration _configuration;
        private readonly TickerService _tickerService;

        public TickerServiceTest()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();
            _tickerService = new TickerService(_httpRequestHandlerMock.Object, _configuration);
        }

        [Fact]
        public async Task Get_StatusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_ticker().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TickerGetRequest tickerGetRequest = new TickerGetRequest
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var resp = await _tickerService.Get(tickerGetRequest);

            //Assert
            Assert.Single(resp);
        }

        [Fact]
        public async Task Get_StatusOk_empty()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TickerGetRequest tickerGetRequest = new TickerGetRequest
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var resp = await _tickerService.Get(tickerGetRequest);

            //Assert
            Assert.Empty(resp);
        }
    }
}
