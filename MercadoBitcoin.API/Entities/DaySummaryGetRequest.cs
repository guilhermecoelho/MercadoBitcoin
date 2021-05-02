using MercadoBitcoin.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoBitcoin.API.Entities
{
    public class DaySummaryGetRequest
    {
        public CoinsEnum Coins { get; set; }

        [Required]
        [Range(typeof(DateTime),"01/01/1970", "31/12/9999", ErrorMessage = "The {0} field is required")]
        public DateTime Date { get; set; }
    }
}
