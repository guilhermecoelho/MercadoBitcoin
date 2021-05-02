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
    public class OrderbookServiceTest
    {
        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private readonly IConfiguration _configuration;
        private readonly IOrderbookService _orderbookService;

        public OrderbookServiceTest()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();
            _orderbookService = new OrderbookService(_httpRequestHandlerMock.Object, _configuration);
        }

        [Fact]
        public async Task Get_StatusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_Orderbook().Build();
            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            OrderbookGetRequest orderbookGetRequest = new OrderbookGetRequest
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var resp = await _orderbookService.Get(orderbookGetRequest);

            //Assert
            Assert.NotNull(resp);
            Assert.Equal(3,resp.Asks.Count);
            Assert.Equal(2, resp.Bids.Count);
        }

        [Fact]
        public async Task Get_StatusOk_empty()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();
            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            OrderbookGetRequest orderbookGetRequest = new OrderbookGetRequest
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var resp = await _orderbookService.Get(orderbookGetRequest);

            //Assert
            Assert.Null(resp);
        }
    }
}
