using MercadoBitcoin.Service.Entities;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public interface IOrderbookService
    {
        Task<Orderbook> Get(OrderbookGetRequest request);
    }
}