using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangerWebProject.Controllers
{
    public class RegistrationController : Controller
    {
        [Route("/registration")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/registration")]
        public IActionResult Register(Registation info)
        {
            if (ModelState.IsValid)
            {
                return View("Accept", info);
            }

            return View();
        }
    }
}
