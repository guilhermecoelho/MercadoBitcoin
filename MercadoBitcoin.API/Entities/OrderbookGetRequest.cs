using MercadoBitcoin.Domain;

namespace MercadoBitcoin.API.Entities
{
    public class OrderbookGetRequest
    {
        public CoinsEnum Coins { get; set; }
    }
}
