using System;

namespace MercadoBitcoin.API.Entities
{
    public class Trades
    {
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int Tid { get; set; }
        public string Type { get; set; }
    }
}
