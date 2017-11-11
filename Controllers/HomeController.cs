using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KriptoFeet.Models;
using KriptoFeet.Categories.DB;
using KriptoFeet.Categories.Models;
using KriptoFeet.Users.Models;
using KriptoFeet.News.DB;

namespace KriptoFeet.Controllers
{
    public class HomeController : Controller
    {
        INewsProvider _newsProvider;
        ICategoriesProvider _categoriesProvider;
        
        public HomeController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
        }

        public ActionResult CreateUser()
        {
            Before();
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User User)
        {
            return View(User);
        }
        public IActionResult Index()
        {
            Before();
            List<Category> categories = _categoriesProvider.GetCategories().Select(c => 
            new Category(c.Name, _newsProvider.GetPopularNewsForCategory(c.Id))).ToList();
            
            ViewBag.LastNews = _newsProvider.GetLastNews();
            return View(categories);
        }

        public IActionResult Сategory(long id)
        {
            Before();
            ViewData["Message"] = "Your contact page.";
            ViewBag.News = _newsProvider.GetNewsDBByCategory(id);
            return View();
        }
        
        public ActionResult SingIn()
        {
            Before();
            return View(new SingInData());
        }

        [HttpPost]
        public ActionResult SingIn(SingInData User)
        {
            Before();
            return View(User);
        }

        public IActionResult News()
        {
            Before();
            ViewData["Message"] = "News page.";

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

        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }
    }
}
