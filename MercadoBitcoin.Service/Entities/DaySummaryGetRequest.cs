using MercadoBitcoin.Domain;
using System;

namespace MercadoBitcoin.Service.Entities
{
    public class DaySummaryGetRequest
    {
        public CoinsEnum Coins { get; set; }
        public DateTime Date { get; set; }
    }
}
