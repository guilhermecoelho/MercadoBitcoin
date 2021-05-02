using System.Net.Http;
using System.Threading.Tasks;

namespace MercadoBitcoin.Infra
{
    public class HttpRequestHandler : IHttpRequestHandler
    {
        private readonly HttpClient _client;

        public HttpRequestHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return response;
        }
    }
}
