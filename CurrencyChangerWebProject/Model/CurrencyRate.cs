using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CurrencyExсhanger.Web.Model
{
    public class CurrencyRate
    {
        public string Txt { get; set; }

        public decimal Rate { get; set; }

        public string Cc { get; set; }
        
        public string ExchangeDate { get; set; }
    }
}
