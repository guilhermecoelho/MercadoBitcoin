using AutoMapper;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using MercadoBitcoin.Test.Builders;
using MercadoBitcoin.Test.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class DaySummaryIntegrationTest
    {
        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        private readonly DaySummaryController _daySummaryController;

        public DaySummaryIntegrationTest()
        {
            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            var services = new StartupHelper().BuildService();
            services.AddSingleton<IDaySummaryService, DaySummaryService>();
            services.AddSingleton(_httpRequestHandlerMock.Object);

            var serviceProvider = services.BuildServiceProvider();
            var daySummaryService = serviceProvider.GetService<IDaySummaryService>();
            var mapper = serviceProvider.GetService<IMapper>();

            _daySummaryController = new DaySummaryController(mapper, daySummaryService);
        }

        [Fact]
        public async Task Get_StatusOk()
        {
            //Arrange
            var expectedResult = new DaySummary
            {
                Date = new DateTime(2013, 06, 20),
                Opening = 262.99999,
                Closing = 269.0,
                Lowest = 260.00002,
                Highest = 269.0,
                Volume = 7253.1336356785,
                Quantity = 27.11390588,
                Amount = 28,
                AvgPrice = 267.5060416518087
            };

            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_DaySummary().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            var request = new DaySummaryGetRequest()
            {
                Coins = Domain.CoinsEnum.BTC,
                Date = DateTime.Now
            };

            //Act
            var result = await _daySummaryController.Get(request);

            var okObjectResult = result as OkObjectResult;
            var resultObj = okObjectResult.Value as DaySummary;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(resultObj);

            Assert.Equal(expectedResult.Date, resultObj.Date);
            Assert.Equal(expectedResult.Opening, resultObj.Opening);
            Assert.Equal(expectedResult.Closing, resultObj.Closing);
            Assert.Equal(expectedResult.Lowest, resultObj.Lowest);
            Assert.Equal(expectedResult.Highest, resultObj.Highest);
            Assert.Equal(expectedResult.Volume, resultObj.Volume);
            Assert.Equal(expectedResult.Quantity, resultObj.Quantity);
            Assert.Equal(expectedResult.Amount, resultObj.Amount);
            Assert.Equal(expectedResult.AvgPrice, resultObj.AvgPrice);

        }
    }
}
