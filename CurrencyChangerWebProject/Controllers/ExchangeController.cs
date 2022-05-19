using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CurrencyRateService _currencyRateService;

        public ExchangeController(AppDbContext context)
        {
            _context = context;
            _currencyRateService = new CurrencyRateService();
        }

        [HttpGet]
        [Authorize]
        [Route("/exchange")]
        public IActionResult ExchangePage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/exchange")]
        public async Task<IActionResult> ExchangePage(ExchangeHistory data)
        {
            if (data == null || data.CurrentCurrencyСС == data.DesiredСurrencyСС)
                return View(data);

            var user = _context.Users.First(item =>
                item.Email == User.Identity.Name);
            data.UserId = user.Id;

            var currenciesRate = await _currencyRateService.GetRatesAsync(DateTime.Now);

            var currentCurrencyObj = currenciesRate.First(item =>
                item.Cc == data.CurrentCurrencyСС);
            var desireCurrencyObj = currenciesRate.First(item =>
                item.Cc == data.DesiredСurrencyСС);

            data.RateOfExchange = Math.Round(ExchangeRate(currentCurrencyObj.Rate, desireCurrencyObj.Rate), 4);
            if (ModelState.IsValid)
            {
                data.DesireCurrencyValue = ExchangeResult(data.CurrentCurrencyValue, data.RateOfExchange);

                await _context.ExchangeHistories.AddAsync(data);
                await _context.SaveChangesAsync();

                return View("SuccessfulExchange", data);
            }

            return View(data);
        }

        [HttpGet]
        [Authorize]
        [Route("/history/exchanges")]
        public IActionResult ExchangesHistory()
        {
            var user = _context.Users.First(item =>
                item.Email == User.Identity.Name);

            var list = _context.ExchangeHistories.Where(item =>
                item.UserId == user.Id).ToList();

            return View(list);
        }

        private decimal ExchangeRate(decimal currentCurrencyRate, decimal disireCurrencyRate)
        {
            var result = currentCurrencyRate / disireCurrencyRate;

            return result;
        }

        private decimal ExchangeResult(decimal currentCurrencyValue, decimal rate)
        {
            var result = Math.Round(currentCurrencyValue * rate, 2);

            return result;
        }
    }
}
