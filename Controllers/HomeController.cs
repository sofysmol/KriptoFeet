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
        private readonly INewsProvider _newsProvider;
        private readonly ICategoriesProvider _categoriesProvider;

        private readonly INewsService _newsService;
        private readonly IUsersService _userService;

        private readonly IProfileService _profileService;

        public HomeController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService,
                              IProfileService profileService)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
            _newsService = newsService;
            _userService = userService;
            _profileService = profileService;
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
        public IActionResult UserProfile()
        {
            Before();
            ViewData["Message"] = "User profile page.";
            return View(_profileService.GetProfile());
        }

        public IActionResult UserProfileSettings()
        {
            Before();
            ViewData["Message"] = "User profile settings page.";

            try
            {
                return View(_userService.GetUserSettings());
            }
            catch (Exception e)
            {
                return View(new UserSettings(null, null, new DateTime(), null, null));
            }
        }

        public IActionResult UserProfileSettings1(PasswordChangingRequest request)
        {
            return UserProfileSettings();
        }

        public IActionResult UserProfileSettings1(UserSettings settings)
        {
            Before();

            ViewData["Message"] = "User profile settings page.";
            if (ModelState.IsValid)
            {
                _userService.UpdateUserSettings(settings);
                return View(settings);
            }
            else return View(new UserSettings(null, null, new DateTime(), null, null));
        }
        public IActionResult ContentManagerProfile()
        {
            Before();
            ViewData["Message"] = "User profile page.";
            return View(_profileService.GetContentManagerProfile());
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
