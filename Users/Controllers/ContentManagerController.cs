using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KriptoFeet.Categories.DB;
using KriptoFeet.Categories.Models;
using KriptoFeet.Users.Models;
using KriptoFeet.News.DB;
using KriptoFeet.News;
using KriptoFeet.News.Models;
using KriptoFeet.Users;
using KriptoFeet.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using KriptoFeet.Utils;

namespace KriptoFeet.Users.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ContentManagerController : Controller
    {
        private readonly INewsProvider _newsProvider;
        private readonly ICategoriesProvider _categoriesProvider;

        private readonly INewsService _newsService;
        private readonly IUsersService _userService;

        private readonly IProfileService _profileService;

        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        private readonly ILongRandomGenerator _rand;

        public ContentManagerController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService,
                              IProfileService profileService,
                              UserManager<Account> userManager,
                              SignInManager<Account> signInManager,
                              ILongRandomGenerator rand)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
            _newsService = newsService;
            _userService = userService;
            _profileService = profileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _rand = rand;
        }

        public ActionResult CreateNews()
        {
            Before();
            return View(new CorrectNewsForm());
        }

        [HttpPost]
        public async Task<ActionResult> CreateNews(CorrectNewsForm news)
        {
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
            var newsDB = new NewsDB{Id = _rand.Next(), Title = news.Title, Body = news.Body,
                AuthorId = user.Id, CategotyId = news.CategoryId, Date = DateTime.Now};
            _newsProvider.AddNewsDB(newsDB);
            } else {
                ModelState.AddModelError(string.Empty, "Не удалось сохранить новость");
            }
            return RedirectToAction("UserProfile", "User");
        }
        public async Task<ActionResult> CorrectNews(long id)
        {
            Before();
            NewsInfo news = await _newsService.GetNews(id);
            return View(new CorrectNewsForm(id, news.Title, news.Body, news.Category.Id, news.Date, news.Comments));
        }

        [HttpPost]
        public async Task<ActionResult> CorrectNews(CorrectNewsForm news)
        {
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            var newsDB = _newsProvider.GetNewsDB(news.Id);
            if (ModelState.IsValid && newsDB != null && user.Id == newsDB.AuthorId)
            {
                newsDB.Title = news.Title;
                newsDB.CategotyId = news.CategoryId;
                newsDB.Body = news.Body;
                newsDB.Date = DateTime.Now;
                _newsProvider.UpdateNewsDB(news.Id, newsDB);
            } else {
                ModelState.AddModelError(string.Empty, "Не удалось сохранить новость");
            }
            return RedirectToAction("UserProfile", "User");
        }

        public async Task<ActionResult> DeleteNews(long id)
        {
            Before();
            var newsDB = _newsProvider.GetNewsDB(id);
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            if (newsDB != null && user.Id == newsDB.AuthorId)
            {
                _newsProvider.DeleteNewsDB(id);
            }else {
                ModelState.AddModelError(string.Empty, "Не удалось сохранить новость");
            }
            return RedirectToAction("UserProfile", "User");
        }

        /*public IActionResult ContentManagerProfile()
        {
            Before();
            ViewData["Message"] = "User profile page.";
            return View(_profileService.GetContentManagerProfile());
        }*/
        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }
    }
}