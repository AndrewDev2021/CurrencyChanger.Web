using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangerWebProject.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
