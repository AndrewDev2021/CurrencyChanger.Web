using CurrencyChangerWebProject.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace CurrencyChangerWebProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public IActionResult HomePage()
        {
            return View();
        }

        [Authorize]
        [Route("/show")]
        public IActionResult ShowUsers()
        {
            var users = _context.Users.First();

            return View(users);

        }
    }
}
