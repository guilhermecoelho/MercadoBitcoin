using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public class TickerService : ITickerService
    {
        private string _url;
        private readonly string TICKER = "ticker";

        private readonly IHttpRequestHandler _httpRequestHandler;
        private readonly IConfiguration _configuration;

        public TickerService(IHttpRequestHandler httpRequestHandler, IConfiguration configuration)
        {
            _httpRequestHandler = httpRequestHandler;
            _configuration = configuration;

            _url = _configuration.GetSection("endpoints").GetSection("mercadobitcoin").Value;
        }

        public async Task<IEnumerable<Ticker>> Get(TickerGetRequest request)
        {
            var tickerList = new List<Ticker>();

            var url = new Utils().BuildUrl(_url, request.Coins.ToString(), TICKER);

            var resp = await _httpRequestHandler.Get(url);

            var respDeserilized =  JsonConvert.DeserializeObject<TickerRoot>(await resp.Content.ReadAsStringAsync());

            if(respDeserilized != null) tickerList.Add(respDeserilized.Ticker);

            return tickerList;
        }
    }

    public class TickerRoot
    {
        public Ticker Ticker { get; set; }
    }
}
