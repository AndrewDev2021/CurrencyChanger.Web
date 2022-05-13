using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CurrencyChangerWebProject.Domain;
using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace CurrencyChangerWebProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/login")]
        public async Task<IActionResult> Login(LogIn info)
        {
            if (ModelState.IsValid)
            {
                Registation user = await _context.Users.FirstOrDefaultAsync(u => u.Email == info.Email && u.Password == info.Password);
                if (user != null)
                {
                    await Authenticate(info.Email); // аутентификация

                    return RedirectToAction("HomePage", "Home");
                }
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(info);
        }

        [Route("/registration")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/registration")]
        public async Task<IActionResult> Register(Registation info)
        {
            if (ModelState.IsValid)
            {
                Registation user = await _context.Users.FirstOrDefaultAsync(u => u.Email == info.Email);
                if (user == null)
                {
                    _context.Users.Add(info);
                    await _context.SaveChangesAsync();
                    await Authenticate(info.Email);

                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "There is already a user with this email");
                }
            }

            return View(info);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("HomePage", "Home");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
