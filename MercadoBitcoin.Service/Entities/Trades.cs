namespace MercadoBitcoin.Service.Entities
{
    public class Trades
    {
        public int Date { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int Tid { get; set; }
        public string Type { get; set; }
    }
}
