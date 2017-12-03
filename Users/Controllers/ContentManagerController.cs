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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace KriptoFeet.Users.Controllers
{
    [Authorize(Roles = "ContentManager,Admin")]
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
        private IHostingEnvironment _hostingEnvironment;

        public ContentManagerController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService,
                              IProfileService profileService,
                              UserManager<Account> userManager,
                              SignInManager<Account> signInManager,
                              IHostingEnvironment hostingEnvironment,
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
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult CreateNews()
        {
            Before();
            return View(new CorrectNewsForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNews(CorrectNewsForm news)
        {
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
            var newsDB = new NewsDB{Id = _rand.Next(), Title = news.Title, Body = news.Body,
                AuthorId = user.Id, CategotyId = news.CategoryId, Date = DateTime.Now};
            _newsProvider.AddNewsDB(newsDB);
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "images", "news", newsDB.Id.ToString());
                if (news.Picture != null && news.Picture.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        await news.Picture.CopyToAsync(stream);
                    }
                }
            } else {
                ModelState.AddModelError(string.Empty, "Не удалось сохранить новость");
            }
            return RedirectToAction("UserProfile", "User");
        }
        
        public async Task<ActionResult> CorrectNews(long id)
        {
            Before();
            NewsInfo news = await _newsService.GetNews(id);
            return View(new CorrectNewsForm(id, news.Title, news.Body, news.Category.Id, news.Date, news.Comments, null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "images", "news", news.Id.ToString());
                if (news.Picture != null && news.Picture.Length > 0)
                {
                    using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        await news.Picture.CopyToAsync(stream);
                    }
                }
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

        public async Task<ActionResult> ContentManagerProfile()
        {
            Before();
            ViewData["Message"] = "User profile page.";
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            if(!roles.Contains("ContentManager"))
                return RedirectToAction("UserProfile", "User");
            return View(await _profileService.GetContentManagerProfile(user.Id));
        }

         public async Task<ActionResult> RefuseContentManagerRole()
        {
            Before();
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            if(roles.Contains("ContentManager"))
            {
                await _userManager.RemoveFromRoleAsync(user, "ContentManager");
                return RedirectToAction("UserProfile", "User");
            }
            return View(await _profileService.GetContentManagerProfile(user.Id));
        }
        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }
    }
}