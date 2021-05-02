using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using MercadoBitcoin.Service.Entities;
using MercadoBitcoin.Test.Builders;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class DaySummaryServiceTest
    {
        private DaySummaryService _daySummaryService;

        private IConfiguration _configuration;
        private Mock<IHttpRequestHandler> _httpRequestHandlerMock;

        public DaySummaryServiceTest()
        {
            _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            _daySummaryService = new DaySummaryService(_httpRequestHandlerMock.Object, _configuration);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_DaySummary().Build();

            _httpRequestHandlerMock.Setup(p => p.Get("https://www.mercadobitcoin.net/api/BTC/day-summary/2021/3/27")).ReturnsAsync(httpResponseMessage);

            var request = new DaySummaryGetRequest()
            {
                Coins = Domain.CoinsEnum.BTC,
                Date = DateTime.Now
            };

            //Act
            var resp = await _daySummaryService.Get(request);

            //Assert
            Assert.NotNull(resp);
        }
    }
}
