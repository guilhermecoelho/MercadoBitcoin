using AutoMapper;
using MercadoBitcoin.API;
using MercadoBitcoin.API.Controllers;
using MercadoBitcoin.API.Entities;
using MercadoBitcoin.Domain;
using MercadoBitcoin.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;
using service = MercadoBitcoin.Service.Entities;

namespace MercadoBitcoin.Test
{
    public class DaySummaryControllerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IDaySummaryService> _daySummaryServiceMock;

        private readonly DaySummaryController _daySummaryController;

        public DaySummaryControllerTest()
        {
            MapperConfig mapperConfig = new MapperConfig();
            _mapper = mapperConfig.MapperConfiguration();
            _daySummaryServiceMock = new Mock<IDaySummaryService>();

            _daySummaryController = new DaySummaryController(_mapper, _daySummaryServiceMock.Object);
        }

        [Fact]
        public async Task Get()
        {
            //Arrange
            _daySummaryServiceMock.Setup(x => x.Get(It.IsAny<service.DaySummaryGetRequest>())).ReturnsAsync(new service.DaySummary
            {
                Date = "2013-06-20",
                Opening = 262.99999,
                Closing = 269.0,
                Lowest = 260.00002,
                Highest = 269.0,
                Volume = 7253.1336356785,
                Quantity = 27.11390588,
                Amount = 28,
                Avg_price = 267.5060416518087
            });

            var expecteResult = new DaySummary
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

            Assert.Equal(expecteResult.Date, resultObj.Date);
            Assert.Equal(expecteResult.Opening, resultObj.Opening);
            Assert.Equal(expecteResult.Closing, resultObj.Closing);
            Assert.Equal(expecteResult.Lowest, resultObj.Lowest);
            Assert.Equal(expecteResult.Highest, resultObj.Highest);
            Assert.Equal(expecteResult.Volume, resultObj.Volume);
            Assert.Equal(expecteResult.Quantity, resultObj.Quantity);
            Assert.Equal(expecteResult.Amount, resultObj.Amount);
            Assert.Equal(expecteResult.AvgPrice, resultObj.AvgPrice);
        }

        [Fact]
        public void Get_Validation()
        {
            var request = new DaySummaryGetRequest()
            {
                Coins = Domain.CoinsEnum.BTC,
                Date = DateTime.Parse("2020-12-12")
            };

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var resp = Validator.TryValidateObject(request, context, results, true);

            Assert.True(resp);
        }

        [Fact]
        public void Get_Validation_Required_Date()
        {
            var request = new DaySummaryGetRequest()
            {
                Coins = Domain.CoinsEnum.BTC
            };

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var resp = Validator.TryValidateObject(request, context, results, true);

            Assert.False(resp);
            Assert.Single(results);
            Assert.Equal("The Date field is required", results[0].ErrorMessage);
        }
    }
}
