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
    public class TickerIntegrationTest
    {
        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private readonly TickerController _tickerController;

        public TickerIntegrationTest()
        {
            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            var services = new StartupHelper().BuildService();
            services.AddSingleton<ITickerService, TickerService>();
            services.AddSingleton(_httpRequestHandlerMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            var tickesService = serviceProvider.GetService<ITickerService>();
            var mapper = serviceProvider.GetService<IMapper>();

            _tickerController = new TickerController(tickesService, mapper);
        }

        [Fact]
        public async Task Get_statusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_ticker().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TickerGetRequest tickerGetRequest = new TickerGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _tickerController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultList = okObjectResult.Value as List<Ticker>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Single(resultList);
        }

        [Fact]
        public async Task Get_statusOk_empty()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            TickerGetRequest tickerGetRequest = new TickerGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _tickerController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultList = okObjectResult.Value as List<Ticker>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Empty(resultList);
        }
    }
}
