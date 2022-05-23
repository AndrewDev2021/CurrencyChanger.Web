using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CurrencyExсhanger.Web.Domain;
using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("/login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/login")]
        public async Task<IActionResult> Login(LogInModel data)
        {
            if (!ModelState.IsValid)
                return View(data);

            var user = await _context.Users
                .Include(u => u.FK_Role_Id)
                .FirstOrDefaultAsync(u => u.Email == data.Email && u.Password == HashingService.GetHashString(data.Password));

            if (user != null)
            {
                await Authenticate(user);

                return RedirectToAction("HomePage", "Home");
            }
            ModelState.AddModelError("", "Incorrect username or password");
            return View(data);
        }

        #endregion

        #region Register-Action

        [HttpGet]
        [Route("/registration")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/registration")]
        public async Task<IActionResult> Register(RegisterModel data)
        {
            if (!ModelState.IsValid) 
                return View(data);

            var userCheck = await _context.Users.FirstOrDefaultAsync(u => u.Email == data.Email);
            if (userCheck == null)
            {
                var user = new User()
                {
                    Email = data.Email,
                    FirstName = data.LastName,
                    LastName = data.LastName,
                    Age = data.Age,
                    Password = HashingService.GetHashString(data.Password),
                    FK_Role_Id = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user")
            };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                await Authenticate(user);

                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                ModelState.AddModelError("", "There is already a user with this email");
            }

            return View(data);
        }

        #endregion

        #region LogOut-Action

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("HomePage", "Home");
        }

        #endregion

        #region Authenticate

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, user.FK_Role_Id.Name)
            };
 
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        #endregion
    }
}
