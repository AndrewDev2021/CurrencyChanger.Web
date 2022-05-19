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
        public async Task<List<CurrencyRate>> GetRatesAsync(DateTime time)
        {
            if (time > DateTime.Now)
            {
                return new List<CurrencyRate>();
            }

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

            var listOfCC = new List<string>();

            foreach (var item in listOfRates)
            {
                listOfCC.Add(item.Cc);
            }

            return listOfCC.ToList();
        }

        public string GetUlr(DateTime time)
        {
            var uFormatTime = time.ToString("u");

            var str= uFormatTime.Remove(10,uFormatTime.Length-10);

            var url = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date={str.Replace("-", "")}&json";
           
            return url;
        }
    }
}
