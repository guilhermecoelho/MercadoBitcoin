using MercadoBitcoin.Service.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public interface ITradesService
    {
        Task<IEnumerable<Trades>> Get(TradesGetRequest request);
    }
}