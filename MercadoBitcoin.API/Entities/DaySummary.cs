using System;

namespace MercadoBitcoin.API.Entities
{
    public class DaySummary
    {
        public DateTime Date { get; set; }
        public double Opening { get; set; }
        public double Closing { get; set; }
        public double Lowest { get; set; }
        public double Highest { get; set; }
        public double Volume { get; set; }
        public double Quantity { get; set; }
        public int Amount { get; set; }
        public double AvgPrice { get; set; }
    }
}
