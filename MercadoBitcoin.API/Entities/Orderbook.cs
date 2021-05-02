using System;
using System.Collections.Generic;

namespace MercadoBitcoin.API.Entities
{
    public class Orderbook
    {
        public List<List<double>> Asks { get; set; }
        public List<List<double>> Bids { get; set; }
    }
}
