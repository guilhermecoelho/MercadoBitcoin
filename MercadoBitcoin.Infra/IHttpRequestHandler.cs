using System.Net.Http;
using System.Threading.Tasks;

namespace MercadoBitcoin.Infra
{
    public interface IHttpRequestHandler
    {
        Task<HttpResponseMessage> Get(string url);
    }
}