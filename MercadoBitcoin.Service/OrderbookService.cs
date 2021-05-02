using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public class OrderbookService : IOrderbookService
    {
        private readonly string _url;
        private readonly string METHOD = "orderbook";

        private readonly IHttpRequestHandler _httpRequestHandler;
        private readonly IConfiguration _configuration;

        public OrderbookService(IHttpRequestHandler httpRequestHandler, IConfiguration configuration)
        {
            _httpRequestHandler = httpRequestHandler;
            _configuration = configuration;

            _url = _configuration.GetSection("endpoints").GetSection("mercadobitcoin").Value;
        }

        public async Task<Orderbook> Get(OrderbookGetRequest request)
        {
            var url = new Utils().BuildUrl(_url, request.Coins.ToString(), METHOD);

            var resp = await _httpRequestHandler.Get(url);

            var respDeserilized = JsonConvert.DeserializeObject<Orderbook>(await resp.Content.ReadAsStringAsync());

            return respDeserilized;

        }
    }
}
