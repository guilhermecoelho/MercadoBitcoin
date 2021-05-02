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
    public class TradesService : ITradesService
    {
        private readonly string _url;
        private readonly string METHOD = "trades";

        private readonly IHttpRequestHandler _httpRequestHandler;
        private readonly IConfiguration _configuration;

        public TradesService(IHttpRequestHandler httpRequestHandler, IConfiguration configuration)
        {
            _httpRequestHandler = httpRequestHandler;
            _configuration = configuration;

            _url = _configuration.GetSection("endpoints").GetSection("mercadobitcoin").Value;
        }

        public async Task<IEnumerable<Trades>> Get(TradesGetRequest request)
        {
            var url = BuildTradesUrl(request);

            var resp = await _httpRequestHandler.Get(url);

            return JsonConvert.DeserializeObject<List<Trades>>(await resp.Content.ReadAsStringAsync());
        }

        public string BuildTradesUrl(TradesGetRequest request)
        {
            var timeStampFrom = new Utils().ConvertDateTimeToTimeStamp(request.From);
            var timeStampTo = new Utils().ConvertDateTimeToTimeStamp(request.To);

            if (request.Tid != 0) return new Utils().BuildUrlWithTid(_url, request.Coins.ToString(), METHOD, request.Tid);
            if (request.From != DateTime.MinValue && request.To != DateTime.MinValue) return new Utils().BuildUrl(_url, request.Coins.ToString(), METHOD, timeStampFrom, timeStampTo);
            if (request.From != DateTime.MinValue) return new Utils().BuildUrl(_url, request.Coins.ToString(), METHOD, timeStampFrom);

            return new Utils().BuildUrl(_url, request.Coins.ToString(), METHOD);
        }
    }
}
