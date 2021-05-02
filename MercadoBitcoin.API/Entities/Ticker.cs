using System;

namespace MercadoBitcoin.API.Entities
{
    public class Ticker
    {
        public double High { get; set; }
        public double Low { get; set; }
        public double Vol { get; set; }
        public double Last { get; set; }
        public double Buy { get; set; }
        public double Sell { get; set; }
        public double Open { get; set; }
        public DateTime Date { get; set; }
    }
}
