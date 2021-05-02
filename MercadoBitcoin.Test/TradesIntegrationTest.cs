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
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class TradesIntegrationTest
    {
        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private readonly TradesController _tradesController;

        public TradesIntegrationTest()
        {
            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            var services = new StartupHelper().BuildService();
            services.AddSingleton<ITradesService, TradesService>();
            services.AddSingleton(_httpRequestHandlerMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            var tradeService = serviceProvider.GetService<ITradesService>();
            var mapper = serviceProvider.GetService<IMapper>();

            _tradesController = new TradesController(mapper, tradeService);
        }

        [Fact]
        public async Task Get_statusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_Trades().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TradesGetRequest tickerGetRequest = new TradesGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _tradesController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultList = okObjectResult.Value as List<Trades>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Equal(5, resultList.Count);
        }

        [Fact]
        public async Task Get_statusOk_Empty() 
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TradesGetRequest tickerGetRequest = new TradesGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _tradesController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultList = okObjectResult.Value as List<Trades>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Empty(resultList);
        }
    }
}
