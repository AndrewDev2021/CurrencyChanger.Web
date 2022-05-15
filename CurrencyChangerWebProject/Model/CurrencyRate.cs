using System.Text.Json.Serialization;

namespace CurrencyExсhanger.Web.Model
{
    public class CurrencyRate
    {
        [JsonPropertyName("txt")]
        public string Name { get; set; }

        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }

        [JsonPropertyName("cc")]
        public string CurrencyCode { get; set; }
    }
}
