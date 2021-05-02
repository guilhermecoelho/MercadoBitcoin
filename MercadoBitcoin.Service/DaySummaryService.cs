using MercadoBitcoin.Domain;
using MercadoBitcoin.Infra;
using MercadoBitcoin.Service.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public class DaySummaryService : IDaySummaryService
    {
        private string _url;
        private readonly string METHOD = "day-summary";

        private readonly IHttpRequestHandler _httpRequestHandler;
        private readonly IConfiguration _configuration;

        public DaySummaryService(IHttpRequestHandler httpRequestHandler, IConfiguration configuration)
        {
            _httpRequestHandler = httpRequestHandler;
            _configuration = configuration;

            _url = _configuration.GetSection("endpoints").GetSection("mercadobitcoin").Value;
        }

        public async Task<DaySummary> Get(DaySummaryGetRequest request)
        {
            var url = new Utils().BuildUrl(_url, request.Coins.ToString(), METHOD, request.Date);

            var resp = await _httpRequestHandler.Get(url);

            if(resp != null) return JsonConvert.DeserializeObject<DaySummary>(await resp.Content.ReadAsStringAsync());

            return new DaySummary();
        }
    }
}
