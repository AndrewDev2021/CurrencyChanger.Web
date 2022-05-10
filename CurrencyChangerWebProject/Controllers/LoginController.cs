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
            return View();
        }
    }
}
