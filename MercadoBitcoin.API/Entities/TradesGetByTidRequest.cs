using MercadoBitcoin.Domain;

namespace MercadoBitcoin.API.Entities
{
    public class TradesGetByTidRequest
    {
        public CoinsEnum Coins { get; set; }
        public double Tid { get; set; }
    }
}
