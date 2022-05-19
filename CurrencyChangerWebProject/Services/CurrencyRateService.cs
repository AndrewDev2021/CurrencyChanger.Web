using CurrencyExсhanger.Web.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Services
{
    public class CurrencyRateService
    {
        public async Task<List<CurrencyRate>> GetRatesAsync(DateTime time)
        {
            var outDate = new DateTime(01, 01, 0001);

            if (time > DateTime.Now || time == outDate)
                return new List<CurrencyRate>();

            var client = new HttpClient();
            var response = await client.GetAsync(GetUlr(time));
            var content = await response.Content.ReadAsStringAsync();

            var list = JsonConvert.DeserializeObject<List<CurrencyRate>>(content);

            list.Add(new CurrencyRate()
            {
                Cc = "UAH",
                ExchangeDate = list.First().ExchangeDate,
                Rate = 1,
                Txt = "Гривня"
            });

            return list;
        }

        public async Task<List<string>> GetCurrenciesCodeAsync()
        {
            var listOfRates = await GetRatesAsync(DateTime.Now);

            return listOfRates.Select(item => item.Cc).ToList();
        }

        private string GetUlr(DateTime time)
        {
            var timeString = time.ToString("u").Remove(10,10);

            return
                $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date={timeString.Replace("-", "")}&json";
        }
    }
}
