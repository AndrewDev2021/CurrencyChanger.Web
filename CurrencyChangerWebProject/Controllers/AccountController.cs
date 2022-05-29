using CurrencyExсhanger.Web.Model;
using CurrencyExсhanger.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CurrencyExсhanger.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        #region LogIn-Action

        [HttpGet]
        [Route("/login")]
        public IActionResult LogIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("HomePage", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/login")]
        public async Task<IActionResult> Login(LogInModel data)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("HomePage", "Home");

            if (!ModelState.IsValid)
                return View(data);

            var result = await _authService.LogInAsync(data);
            if (result == null)
                return RedirectToAction("HomePage", "Home");

            ModelState.AddModelError("", result);

            return View(data);
        }

        #endregion

        #region Register-Action

        [HttpGet]
        [Route("/registration")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("HomePage", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/registration")]
        public async Task<IActionResult> Register(RegisterModel data)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("HomePage", "Home");

            if (!ModelState.IsValid)
                return View(data);

            var result = await _authService.RegisterAsync(data);
            if (result == null)
                return RedirectToAction("HomePage", "Home");

            ModelState.AddModelError("", result);
            return View(data);
        }

        #endregion

        #region LogOut-Action

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogIn", "Account");
            await _authService.LogoutAsync();
            return RedirectToAction("HomePage", "Home");
        }

        #endregion
    }
}
