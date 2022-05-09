using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangerWebProject.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Registation info)
        {
            return View("Accept", info);
        }
    }
}
