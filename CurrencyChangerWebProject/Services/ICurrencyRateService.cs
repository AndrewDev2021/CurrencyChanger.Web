using CurrencyExсhanger.Web.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Services
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRate>> GetRatesAsync(DateTime time);
        Task<List<string>> GetCurrenciesCodeAsync();
        Task<List<ExchangeHistory>> ExchangesHistoryAsync(int userId);
        Task<ExchangeHistory> AddExchangeAsync(ExchangeHistory model);
    }
}