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
        [HttpGet("login")]
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

            await Authenticate(loginIn);
            return LocalRedirect("~/");
        }

        private async Task Authenticate(LoginIn loginIn)
        {
            var claims = new List<Claim>
            {
                new Claim("email", loginIn.Email),
                new Claim("password", loginIn.Password)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
