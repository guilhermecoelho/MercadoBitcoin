using AutoMapper;
using MercadoBitcoin.API;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using service = MercadoBitcoin.Service.Entities;
using api = MercadoBitcoin.API.Entities;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Domain;

namespace MercadoBitcoin.Test
{
    public class TickerControllerTest
    {
        private readonly IMapper _mapper;

        private readonly Mock<ITickerService> _tickerServiceMock;
        private readonly TickerController _tickerController;

        public TickerControllerTest()
        {
            MapperConfig mapperConfig = new MapperConfig();
            _mapper = mapperConfig.MapperConfiguration();

            _tickerServiceMock = new Mock<ITickerService>();

            _tickerController = new TickerController(_tickerServiceMock.Object, _mapper);

        }
        [Fact]
        public async Task Get()
        {
            //Arrange
            _tickerServiceMock.Setup(p => p.Get(It.IsAny<service.TickerGetRequest>())).ReturnsAsync(new List<service.Ticker>
            {
                new service.Ticker
                {
                    High = 1
                }
            });

            //api.TickerGetRequest tickerGetRequest = new TickerGetRequest()
            //{
            //    Coins = CoinsEnum.BTC
            //};

            api.TickerGetRequest tickerGetRequest = null;

            //Act
            var result = await _tickerController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultList = okObjectResult.Value as List<api.Ticker>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.Single(resultList);
        }
    }
}
