using System;
using System.Collections.Generic;
using CurrencyExсhanger.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;

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
            var listOfRates = await _currencyRateService.GetRatesAsync();
            
            return View(listOfRates);
        }
    }
}
