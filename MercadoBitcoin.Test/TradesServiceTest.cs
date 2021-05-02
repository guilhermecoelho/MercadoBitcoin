using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service;
using MercadoBitcoin.Service.Entities;
using MercadoBitcoin.Test.Builders;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class TradesServiceTest
    {
        private string _url;
        private readonly string _method = "trades";

        private readonly TradesService _tradesService;
        private readonly IConfiguration _configuration;

        private readonly Mock<IHttpRequestHandler> _httpRequestHandlerMock;
        public TradesServiceTest()
        {
           _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            _url = _configuration.GetSection("endpoints").GetSection("mercadobitcoin").Value;

            _httpRequestHandlerMock = new Mock<IHttpRequestHandler>();

            _tradesService = new TradesService(_httpRequestHandlerMock.Object, _configuration);

        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_Trades().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            var request = new TradesGetRequest
            {
                Coins = Domain.CoinsEnum.BTC
            };
            //Act
            var resp = await _tradesService.Get(request);

            //Assert
            Assert.NotNull(resp);
            Assert.Equal(5,resp.ToList().Count);
        }

        [Fact]
        public async Task Get_FilterByTid()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_Trades().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            var request = new TradesGetRequest
            {
                Coins = Domain.CoinsEnum.BTC,
                Tid = 1
            };
            //Act
            var resp = await _tradesService.Get(request);

            //Assert
            Assert.NotNull(resp);
            Assert.Equal(5, resp.ToList().Count);
        }

        [Fact]
        public async Task Get_Empty()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOkEmpty().Build();

            _httpRequestHandlerMock.Setup(p => p.Get(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);

            var request = new TradesGetRequest
            {
                Coins = CoinsEnum.BTC
            };
            //Act
            var resp = await _tradesService.Get(request);

            //Assert
            Assert.Null(resp);
        }

        [Fact]
        public void BuildTradesUrl()
        {
            //Arrange
            var coin = CoinsEnum.BTC;

            var expectedUrl = $"{_url}/{coin}/{_method}";

            var request = new TradesGetRequest
            {
                Coins = coin
            };

            //Act
            var resp =  _tradesService.BuildTradesUrl(request);

            //Assert
            Assert.Equal(expectedUrl, resp);
        }

        [Fact]
        public void BuildTradesUrl_with_tid()
        {
            //Arrange
            var coin = CoinsEnum.BTC;
            var tid = 1;

            var expectedUrl = $"{_url}/{coin}/{_method}/?tid={tid}";

            var request = new TradesGetRequest
            {
                Coins = coin,
                Tid = 1
            };

            //Act
            var resp = _tradesService.BuildTradesUrl(request);

            //Assert
            Assert.Equal(expectedUrl, resp);
        }

        [Fact]
        public void BuildTradesUrl_with_From()
        {
            //Arrange
            var coin = CoinsEnum.BTC;
            var dateTimeFrom = new DateTime(2021, 03, 20, 17, 23, 24);
            var timeStampFrom = new Utils().ConvertDateTimeToTimeStamp(dateTimeFrom);

            var expectedUrl = $"{_url}/{coin}/{_method}/{timeStampFrom}";

            var request = new TradesGetRequest
            {
                Coins = coin,
                From = dateTimeFrom
            };

            //Act
            var resp = _tradesService.BuildTradesUrl(request);

            //Assert
            Assert.Equal(expectedUrl, resp);
        }

        [Fact]
        public void BuildTradesUrl_with_From_and_To()
        {
            //Arrange
            var coin = CoinsEnum.BTC;
            var dateTimeFrom = new DateTime(2021, 03, 20, 17, 23, 24);
            var dateTimeTo = new DateTime(2021, 03, 22, 17, 23, 24);
            var timeStampFrom = new Utils().ConvertDateTimeToTimeStamp(dateTimeFrom);
            var timeStampTo = new Utils().ConvertDateTimeToTimeStamp(dateTimeTo);


            var expectedUrl = $"{_url}/{coin}/{_method}/{timeStampFrom}/{timeStampTo}";

            var request = new TradesGetRequest
            {
                Coins = coin,
                From = dateTimeFrom,
                To = dateTimeTo
            };

            //Act
            var resp = _tradesService.BuildTradesUrl(request);

            //Assert
            Assert.Equal(expectedUrl, resp);
        }
    }
}
