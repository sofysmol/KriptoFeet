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
using KriptoFeet.News;
using KriptoFeet.News.Models;
using KriptoFeet.Users;
using KriptoFeet.Exceptions;

namespace KriptoFeet.Controllers
{
    public class HomeController : Controller
    {
        INewsProvider _newsProvider;
        ICategoriesProvider _categoriesProvider;

        INewsService _newsService;
        IUsersService _userService;

        public HomeController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
            _newsService = newsService;
            _userService = userService;
        }

        public ActionResult CreateUser()
        {
            Before();
            return View(new User());
        }

        [HttpPost]
        public ActionResult CreateUser(User User)
        {
            Before();
            if (!User.Agreement)
            {
                ModelState.AddModelError("Agreement", "Без согласия невозможно выполнить регистрацию");
                return View(User);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.CreateUser(User);
                    return View(User);
                }
                catch (NicknameException e)
                {
                    ModelState.AddModelError("Nickname", "Пользоватеь с таким ником уже существует");
                    return View(User);
                }
                catch (EmailException e)
                {
                    ModelState.AddModelError("Email", "Пользоватеь с таким Email уже существует");
                    return View(User);
                }
            }
            else return View(User);
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

        public ActionResult SignIn()
        {
            Before();
            return View(new SignInData());
        }

        [HttpPost]
        public ActionResult SignIn(SignInData User)
        {
            Before();
            if (ModelState.IsValid)
            {
                return View(User);
            }
            else return View(User);
        }

        public IActionResult News(long id)
        {
            Before();
            ViewData["Message"] = "News page.";
            NewsInfo news = _newsService.GetNews(id);
            return View(news);
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
