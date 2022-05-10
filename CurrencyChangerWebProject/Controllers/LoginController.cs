using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CurrencyChangerWebProject.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("/login")]
        public IActionResult LogIn()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return LocalRedirect("~/");
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginIn loginIn)
        {

            //check creds

            return LocalRedirect("~/");
        }
    }
}
