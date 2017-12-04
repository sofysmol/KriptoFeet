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
using KriptoFeet.Users.DB;

namespace KriptoFeet.Users.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController: Controller
    {
        private readonly INewsProvider _newsProvider;
        private readonly ICategoriesProvider _categoriesProvider;

        private readonly INewsService _newsService;
        private readonly IUsersService _userService;

        private readonly IProfileService _profileService;

        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        private readonly ILongRandomGenerator _rand;

        private readonly IContentManagerRequestProvider _requestProveder;

        public AdminController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService,
                              IProfileService profileService,
                              UserManager<Account> userManager,
                              SignInManager<Account> signInManager,
                              ILongRandomGenerator rand,
                              IContentManagerRequestProvider requestProveder)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
            _newsService = newsService;
            _userService = userService;
            _profileService = profileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _rand = rand;
            _requestProveder = requestProveder;
        }

        public ActionResult CreateGroup()
        {
            Before();
            return View(new CreateGroupView());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(string name)
        {
            Before();
            _categoriesProvider.AddCategory(new CategoryDB{Id = _rand.Next(), Name = name});
            return RedirectToAction("AdminProfile", "Admin");
        }

        public ActionResult EditGroup(long id)
        {
            Before();
            var category = _categoriesProvider.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(CategoryDB category)
        {
            Before();
            _categoriesProvider.UpdateCategory(category.Id, category);
            return RedirectToAction("AdminProfile", "Admin");
        }

        public ActionResult DeleteGroup(long id)
        {
            Before();
            var category = _categoriesProvider.GetCategory(id);
            var news = _newsProvider.GetNewsDBByCategory(id);
            if(news != null)
            {
                foreach(var n in news)
                {
                    _newsService.DeleteNews(n.Id);
                }
            }
            _categoriesProvider.DeleteCategory(id);
            return RedirectToAction("AdminProfile", "Admin");
        }

        public async Task<IActionResult> ApproveContentManagerRequest(string id)
        {
            Before();
            var request = _requestProveder.GetRequest(id);
            if(request != null)
            {
                _requestProveder.DeleteRequest(id);
                var user = await _userManager.FindByIdAsync(id);
                if(user != null)
                {
                var roles = await _userManager.GetRolesAsync(user);
                if(roles.Contains("User"))
                    await _userManager.AddToRoleAsync(user, "ContentManager");
                }
            }

            return RedirectToAction("AdminProfile", "Admin");
        }

        public async Task<IActionResult> DeleteContentManager(string id)
        {
            Before();
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if(roles.Contains("ContentManager"))
                    await _userManager.RemoveFromRoleAsync(user, "ContentManager");
            }
            return RedirectToAction("AdminProfile", "Admin");
        }

        public IActionResult DeleteRequest(string id)
        {
            Before();
            _requestProveder.DeleteRequest(id);
            return RedirectToAction("AdminProfile", "Admin");
        }

        public async Task<IActionResult> AdminProfile()
        {
            Before();
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            if(!roles.Contains("Admin"))
                return RedirectToAction("UserProfile", "User");
            ViewData["Message"] = "User profile page.";
            return View(await _profileService.GetAdminProfile(user.Id));
        }

        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }
    }
}