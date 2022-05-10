using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CurrencyChangerWebProject.Domain;
using CurrencyChangerWebProject.Model;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyChangerWebProject.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult HomePage()
        {
            return View();
        }


        [Route("/show")]
        public IActionResult ShowUsers()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var users = db.Users.First();

                return View(users);
            }
        }
    }
}
