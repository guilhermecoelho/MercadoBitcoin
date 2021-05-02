using MercadoBitcoin.Domain;
using System;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class UtilsTest
    {
        private readonly string _url = "http://www.url.com";
        private readonly string _coin = "BTC";
        private readonly string _method = "method";

        private readonly Utils _utils;

        public UtilsTest()
        {
            _utils = new Utils();
        }

        [Fact]
        public void BuildUrl()
        {

            var expectedUrl = $"{_url}/{_coin}/{_method}";

            var result = _utils.BuildUrl(_url, _coin, _method);

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void BuildUrl_withFrom()
        {
            var from = 1502993741;

            var expectedUrl = $"{_url}/{_coin}/{_method}/{from}";

            var result = _utils.BuildUrl(_url, _coin, _method, from);

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void BuildUrl_withFromAndDate()
        {
            var from = 1502993741;
            var to = 1616865804;

            var expectedUrl = $"{_url}/{_coin}/{_method}/{from}/{to}";

            var result = _utils.BuildUrl(_url, _coin, _method, from, to);

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void BuildUrlWithTid()
        {
            var tid = 9558565;

            var expectedUrl = $"{_url}/{_coin}/{_method}/?tid={tid}";

            var result = _utils.BuildUrlWithTid(_url, _coin, _method, tid);

            Assert.Equal(expectedUrl, result);
        }

        [Fact]
        public void BuildUlrWithDate()
        {
            var dateTime = DateTime.Now;

            var expectedUrl = $"{_url}/{_coin}/{_method}/{dateTime.Year}/{dateTime.Month}/{dateTime.Day}";

            var result = _utils.BuildUrl(_url, _coin, _method, dateTime);

            Assert.Equal(expectedUrl, result);
        }

        [Theory]
        [InlineData(1502993741)]
        [InlineData(1616865804)]
        [InlineData(1616261004)]
        public void ConverterTimestampToDatetime(double timestamp)
        {
            //Arrange
            DateTime expectedResult = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            expectedResult = expectedResult.AddSeconds(timestamp);

            //Act
            var result = _utils.ConverterTimestampToDatetime(timestamp);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ConvertDateTimeToTimeStamp()
        {
            //20/03/2021 17:23:24
            //1616261004

            //Arrange
            var datetime = new DateTime(2021, 03, 20, 17, 23, 24);
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            var expectedResult = (double)span.TotalSeconds;

            //Act
            var result = _utils.ConvertDateTimeToTimeStamp(datetime);

            //Assert
            Assert.Equal(expectedResult, result);

        }
    }
}
