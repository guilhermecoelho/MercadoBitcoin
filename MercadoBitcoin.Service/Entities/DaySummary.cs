using System.Text.Json.Serialization;

namespace MercadoBitcoin.Service.Entities
{
    public class DaySummary
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("opening")]
        public double Opening { get; set; }

        [JsonPropertyName("closing")]
        public double Closing { get; set; }

        [JsonPropertyName("lowest")]
        public double Lowest { get; set; }

        [JsonPropertyName("highest")]
        public double Highest { get; set; }

        [JsonPropertyName("volume")]
        public double Volume { get; set; }

        [JsonPropertyName("quantity")]
        public double Quantity { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("avg_price")]
        public double Avg_price { get; set; }
    }
}
