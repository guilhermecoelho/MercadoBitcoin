using AutoMapper;
using MercadoBitcoin.API;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.Domain;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using api = MercadoBitcoin.API.Entities;
using service = MercadoBitcoin.Service.Entities;

namespace MercadoBitcoin.Test
{
    public class OrderbookControllerTest
    {
        private readonly IMapper _mapper;

        private readonly Mock<IOrderbookService> _orderbookService;
        private readonly OrderbookController _orderbookController;

        public OrderbookControllerTest()
        {
            MapperConfig mapperConfig = new MapperConfig();
            _mapper = mapperConfig.MapperConfiguration();

            _orderbookService = new Mock<IOrderbookService>();

            _orderbookController = new OrderbookController(_orderbookService.Object, _mapper);

        }
        [Fact]
        public async Task Get()
        {
            //Arrange
            _orderbookService.Setup(p => p.Get(It.IsAny<service.OrderbookGetRequest>())).ReturnsAsync(new service.Orderbook
            {
                Asks = new List<List<double>>
               {
                   new List<double>
                   {
                       25,32
                   }
               },
                Bids = new List<List<double>>
               {
                   new List<double>
                   {
                       25,32,25
                   }
               }
            });

            api.OrderbookGetRequest tickerGetRequest = new api.OrderbookGetRequest()
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _orderbookController.Get(tickerGetRequest);

            var okObjectResult = result as OkObjectResult;
            var resultObj = okObjectResult.Value as api.Orderbook;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(resultObj);
            Assert.Single(resultObj.Asks);
            Assert.Single(resultObj.Bids);
        }
    }
}
