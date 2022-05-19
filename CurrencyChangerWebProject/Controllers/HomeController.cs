using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private CurrencyRateService _currencyRateService;

        public HomeController(AppDbContext context)
        {
            _context = context;
            _currencyRateService = new CurrencyRateService();
        }

        [HttpGet]
        [Route("/")]
        public IActionResult HomePage()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("ExchangePage", "Exchange");

            return View();

        }

        [HttpGet]
        [Authorize]
        [Route("/show")]
        public IActionResult ShowUsers()
        {
            var users = _context.Users.Select(u => u).ToList();

            return View(users);

        }

        [HttpGet]
        [Authorize]
        [Route("/rate")]
        public async Task<IActionResult> CurrencyRate()
        {
            var listOfRates = await _currencyRateService.GetRatesAsync(DateTime.Now);

            return View(listOfRates);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/rate")]
        public async Task<IActionResult> CurrencyRate(DateTime date)
        {
            var listOfRates = await _currencyRateService.GetRatesAsync(date);

            return View(listOfRates);
        }

        [HttpGet]
        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
