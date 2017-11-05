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
