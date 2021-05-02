using MercadoBitcoin.Domain;
using System;

namespace MercadoBitcoin.Service.Entities
{
    public class TradesGetRequest
    {
        public CoinsEnum Coins { get; set; }
        public double Tid { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
