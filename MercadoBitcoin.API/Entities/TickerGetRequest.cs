using MercadoBitcoin.Domain;
using System.ComponentModel.DataAnnotations;

namespace MercadoBitcoin.API.Entities
{
    public class TickerGetRequest
    {
        [Required]
        public CoinsEnum Coins { get; set; }
    }
}
