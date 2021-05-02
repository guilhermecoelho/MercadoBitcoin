using MercadoBitcoin.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoBitcoin.API.Entities
{
    public class TradesGetRequest
    {
        public CoinsEnum Coins { get; set; }
        [Required]
        [Range(typeof(DateTime), "01/01/1970", "31/12/9999", ErrorMessage = "The {0} field is required")]
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
