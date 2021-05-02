using AutoMapper;
using MercadoBitcoin.API;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Domain;
using MercadoBitcoin.Service;
using MercadoBitcoin.Test.Builders;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using service = MercadoBitcoin.Service.Entities;

namespace MercadoBitcoin.Test
{
    public class TradesControllerTest
    {
        private readonly IMapper _mapper;
        private readonly TradesController _tradesController;
        private readonly Mock<ITradesService> _tradesServiceMock;

        public TradesControllerTest()
        {
            MapperConfig mapperConfig = new MapperConfig();
            _mapper = mapperConfig.MapperConfiguration();
            _tradesServiceMock = new Mock<ITradesService>();
            _tradesController = new TradesController(_mapper, _tradesServiceMock.Object);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange

            var tradesMock = new TradesResponseBuilder().Build();

            var dateConvert = new  Utils().ConverterTimestampToDatetime(tradesMock.FirstOrDefault().Date);

            _tradesServiceMock.Setup(x => x.Get(It.IsAny<service.TradesGetRequest>())).ReturnsAsync(tradesMock);

            var request = new TradesGetRequest
            {
                Coins = CoinsEnum.BTC
            };

            //Act
            var result = await _tradesController.Get(request);

            var okObjectResult = result as OkObjectResult;
            var resultObj = okObjectResult.Value as List<Trades>;

            //Assert
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(resultObj);
            Assert.Equal(5, resultObj.Count);

            Assert.Equal(dateConvert, resultObj.FirstOrDefault().Date);
            Assert.Equal(tradesMock.FirstOrDefault().Price, resultObj.FirstOrDefault().Price);
            Assert.Equal(tradesMock.FirstOrDefault().Amount, resultObj.FirstOrDefault().Amount);
            Assert.Equal(tradesMock.FirstOrDefault().Tid, resultObj.FirstOrDefault().Tid);
            Assert.Equal(tradesMock.FirstOrDefault().Type, resultObj.FirstOrDefault().Type);
        }

        //[Fact]
        //public async Task Get_FilterByTid()
        //{
        //    //Arrange
        //    var tid = 797860;
        //    var tradesMock = new TradesResponseBuilder().FilterByTid(tid).Build();
        //    _tradesServiceMock.Setup(x => x.Get(It.IsAny<service.TradesGetRequest>())).ReturnsAsync(tradesMock);

        //    var request = new TradesGetByTidRequest
        //    {
        //        Coins = CoinsEnum.BTC,
        //        Tid = tid
        //    };

        //    //Act
        //    var result = await _tradesController.GetByTid(request);

        //    var okObjectResult = result as OkObjectResult;
        //    var resultObj = okObjectResult.Value as List<Trades>;

        //    //Assert
        //    Assert.Equal(200, okObjectResult.StatusCode);
        //    Assert.NotNull(resultObj);
        //    Assert.Equal(3, resultObj.Count);

        //    Assert.True(resultObj.FirstOrDefault().Tid >= tid);
        //    Assert.True(resultObj.LastOrDefault().Tid >= tid);
        //}

        [Fact]
        public void Get_Validation()
        {
            var request = new TradesGetRequest
            {
                Coins = CoinsEnum.BTC,
                From = DateTime.Now,
                To = DateTime.Now
            };

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var resp = Validator.TryValidateObject(request, context, results, true);

            Assert.True(resp);
        }

        [Fact]
        public void Get_Validation_Required_From()
        {
            var request = new TradesGetRequest
            {
                Coins = CoinsEnum.BTC,
                To = DateTime.Now
            };

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var resp = Validator.TryValidateObject(request, context, results, true);

            Assert.False(resp);
            Assert.Single(results);
            Assert.Equal("The From field is required", results[0].ErrorMessage);
        }
    }
}
