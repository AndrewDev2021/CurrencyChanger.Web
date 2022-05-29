using CurrencyExсhanger.Web.Extensions;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Controllers
{
    [Authorize]
    public class ExchangeController : Controller
    {
        private readonly ICurrencyRateService _currencyRateService;
        public ExchangeController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        [HttpGet]
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

            var result = await _currencyRateService.AddExchangeAsync(data);

            return View("SuccessfulExchange", data);
        }

        [HttpGet]
        [Route("/history/exchanges")]
        public async Task<IActionResult> ExchangesHistory()
        {
            var result = await _currencyRateService.ExchangesHistoryAsync(User.GetUserId());
            return View(result);
        }
    }
}
