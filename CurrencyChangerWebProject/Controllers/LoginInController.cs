using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangerWebProject.Controllers
{
    public class LoginInController : Controller
    {
        public IActionResult LoginIn()
        {
            return View();
        }
    }
}
