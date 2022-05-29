using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Extensions;
using CurrencyExсhanger.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Services
{
    public class CurrencyRateService : ICurrencyRateService, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _db;
        public CurrencyRateService(HttpClient httpClient, AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _db = db;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<List<CurrencyRate>> GetRatesAsync(DateTime time)
        {
            var outDate = new DateTime(01, 01, 0001);

            if (time > DateTime.Now || time == outDate)
                return new List<CurrencyRate>();

            var response = await _httpClient.GetAsync(GetUlr(time));
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

        public async Task<List<ExchangeHistory>> ExchangesHistoryAsync(int userId)
        {
            return await _db.ExchangeHistories.Where(item => item.UserId == userId).ToListAsync();
        }

        private string GetUlr(DateTime date)
        {
            return $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date={date.ToString("yyyyyMMdd")}&json";
        }

        public async Task<ExchangeHistory> AddExchangeAsync(ExchangeHistory model)
        {
            model.UserId = _httpContext.User.GetUserId();

            var currenciesRate = await GetRatesAsync(DateTime.Now);

            var currentCurrencyObj = currenciesRate.First(item =>
                item.Cc == model.CurrentCurrencyСС);
            var desireCurrencyObj = currenciesRate.First(item =>
                item.Cc == model.DesiredСurrencyСС);

            model.RateOfExchange = Math.Round(ExchangeRate(currentCurrencyObj.Rate, desireCurrencyObj.Rate), 4);

            model.DesireCurrencyValue = ExchangeResult(model.CurrentCurrencyValue, model.RateOfExchange);

            await _db.ExchangeHistories.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        private decimal ExchangeRate(decimal currentCurrencyRate, decimal desireCurrencyRate)
        {
            return currentCurrencyRate / desireCurrencyRate;
        }

        private decimal ExchangeResult(decimal currentCurrencyValue, decimal rate)
        {
            return Math.Round(currentCurrencyValue * rate, 2);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
