using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExсhanger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        #region LogIn-Action

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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == info.Email
                    && u.Password == HashingService.GetHashString(info.Password));
                if (user != null)
                {
                    await Authenticate(info.Email); // аутентификация

                    return RedirectToAction("HomePage", "Home");
                }
                ModelState.AddModelError("", "Incorrect username or password");
            }
            return View(info);
        }

        #endregion

        #region Register-Action

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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == info.Email);
                if (user == null)
                {
                    info.Password = HashingService.GetHashString(info.Password);

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

        #endregion

        #region LogOut-Action
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("HomePage", "Home");
        }

        #endregion

        #region Authenticate

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
 
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        #endregion
    }
}
