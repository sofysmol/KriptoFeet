using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KriptoFeet.Models;
using KriptoFeet.Users.Models;

namespace KriptoFeet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult CreateUser()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User User)
        {
            return View(User);
        }
      

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Сategory()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        
        public ActionResult SingIn()
        {
            return View(new SingInData());
        }

        [HttpPost]
        public ActionResult SingIn(SingInData User)
        {
            return View(User);
        }

        public IActionResult News()
        {
            ViewData["Message"] = "News page.";

            return View();
        }
        public IActionResult UserProfile()
        {
            ViewData["Message"] = "User profile page.";

            return View();
        }

        public IActionResult UserProfileSettings()
        {
            ViewData["Message"] = "User profile settings page.";

            return View();
        }
        public IActionResult ContentManagerProfile()
        {
            ViewData["Message"] = "User profile settings page.";

            return View();
        }
        public IActionResult AdminProfile()
        {
            ViewData["Message"] = "User profile page.";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
