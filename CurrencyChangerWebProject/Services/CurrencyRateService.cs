using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExсhanger.Web.Model;
using Newtonsoft.Json;

namespace CurrencyExсhanger.Web.Services
{
    public class CurrencyRateService
    {
        private const string CurrencyRateApiUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";

        public async Task<List<CurrencyRate>> GetRatesAsync()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(CurrencyRateApiUrl);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<CurrencyRate>>(content);
        }
    }
}
