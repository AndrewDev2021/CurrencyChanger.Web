using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyExсhanger.Web.Model;
using Microsoft.AspNetCore.Mvc;
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
            
            var list = JsonConvert.DeserializeObject<List<CurrencyRate>>(content);

            list.Add(new CurrencyRate()
            {
                Cc = "UAH",
                ExchangeDate = (DateTime.Now).ToString(),
                Rate = 1,
                Txt = "Гривня"
            });

            return list;
        }

        public async Task<List<string>> GetCurrenciesCodeAsync()
        {
            var listOfRates = await GetRatesAsync();

            var listOfCC = new List<string>();

            foreach (var item in listOfRates)
            {
                listOfCC.Add(item.Cc);
            }

            return listOfCC.ToList();
        }
    }
}
