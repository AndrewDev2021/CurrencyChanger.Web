using CurrencyChangerWebProject.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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


        [Route("/show")]
        public IActionResult ShowUsers()
        {

            var users = _context.Users.First();

            return View(users);

        }
    }
}
