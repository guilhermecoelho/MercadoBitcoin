using MercadoBitcoin.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MercadoBitcoin.Service
{
    public interface ITickerService
    {
        Task<IEnumerable<Ticker>> Get(TickerGetRequest request);
    }
}
