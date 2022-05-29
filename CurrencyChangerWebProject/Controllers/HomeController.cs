using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private ICurrencyRateService _currencyRateService;

        public HomeController(AppDbContext context, ICurrencyRateService currencyRateService)
        {
            _context = context;
            _currencyRateService = currencyRateService;
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
        [Route("/rate")]
        public async Task<IActionResult> CurrencyRate()
        {
            return View(await _currencyRateService.GetRatesAsync(DateTime.Now));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/rate")]
        public async Task<IActionResult> CurrencyRate(DateTime date)
        {
            return View(await _currencyRateService.GetRatesAsync(date));
        }

        [HttpGet]
        [Route("/about")]
        public IActionResult ContactUs()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/about")]
        public async Task<IActionResult> ContactUs(ContactUs data)
        {
            if (!ModelState.IsValid)
                return View();

            await _context.ContactUsMessages.AddAsync(data);
            await _context.SaveChangesAsync();

            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("/show/message")]
        public async Task<IActionResult> ShowMessage()
        {
            return View(await _context.ContactUsMessages.ToListAsync());
        }
    }
}
