using AutoMapper;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using MercadoBitcoin.Test.Builders;
using MercadoBitcoin.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class OrderbookIntegrationTest
    {
        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private readonly OrderbookController _orderbookController;
        public OrderbookIntegrationTest()
        {
            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            var services = new StartupHelper().BuildService();
            services.AddSingleton<IOrderbookService, OrderbookService>();
            services.AddSingleton(_httpRequestHandlerMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            var orderbookService = serviceProvider.GetService<IOrderbookService>();
            var mapper = serviceProvider.GetService<IMapper>();

            _orderbookController = new OrderbookController(orderbookService, mapper);
        }

        [Fact]
        public async Task Get_statusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_Orderbook().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            OrderbookGetRequest orderbookGetRequest = new OrderbookGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _orderbookController.Get(orderbookGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultObj = okObjectResult.Value as Orderbook;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(resultObj);
            Assert.Equal(3, resultObj.Asks.Count);
            Assert.Equal(2, resultObj.Bids.Count);
        }

        [Fact]
        public async Task Get_statusOk_empty()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            OrderbookGetRequest orderbookGetRequest = new OrderbookGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _orderbookController.Get(orderbookGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultObj = okObjectResult.Value as Orderbook;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Null(resultObj);
        }
    }
}
