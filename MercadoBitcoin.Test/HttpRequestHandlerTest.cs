using MercadoBitcoin.Infra;
using MercadoBitcoin.Service.Entities;
using MercadoBitcoin.Test.Builders;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MercadoBitcoin.Test
{
    public class HttpRequestHandlerTest
    {
        private HttpClient _client;
        private readonly string URL = "https://www.mercadobitcoin.net/api/BTC/orderbook";

        [Fact]
        public async Task Get_statusOk()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessageBuilder().StatusOk_ticker().Build();

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).ReturnsAsync(httpResponseMessage);

            _client = new HttpClient(handlerMock.Object);
            HttpRequestHandler requestHandler = new HttpRequestHandler(_client);

            //Act
            var resp = await requestHandler.Get(URL);

            //Assert
            Assert.NotNull(resp);
        }
    }
}
