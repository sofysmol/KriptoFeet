using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KriptoFeet.Categories.DB;
using KriptoFeet.Categories.Models;
using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using KriptoFeet.News.DB;
using KriptoFeet.News;
using KriptoFeet.News.Models;
using KriptoFeet.Users;
using KriptoFeet.Comments;
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
    [Authorize]
    public class UserController : Controller
    {
        private readonly INewsProvider _newsProvider;
        private readonly ICategoriesProvider _categoriesProvider;

        private readonly INewsService _newsService;
        private readonly IUsersService _userService;

        private readonly IProfileService _profileService;
        private readonly ICommentsService _commentService;

        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        private readonly ILongRandomGenerator _rand;

        public UserController(INewsProvider newsProvider,
                              ICategoriesProvider categoriesProvider,
                              INewsService newsService,
                              IUsersService userService,
                              IProfileService profileService,
                              UserManager<Account> userManager,
                              SignInManager<Account> signInManager,
                              ICommentsService commentService,
                              ILongRandomGenerator rand)
        {
            _newsProvider = newsProvider;
            _categoriesProvider = categoriesProvider;
            _newsService = newsService;
            _userService = userService;
            _profileService = profileService;
            _userManager = userManager;
            _signInManager = signInManager;
            _commentService = commentService;
            _rand = rand;
        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(PasswordChangingRequest model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var t = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                if (t)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<Account>)) as IPasswordValidator<Account>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<Account>)) as IPasswordHasher<Account>;

                    IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, model.OldPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неверный пароль");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }

            return View(model);
        }

        public async Task<IActionResult> UserProfile()
        {
            Before();
            ViewData["Message"] = "User profile page.";
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            if(roles.Contains("Admin"))
                return RedirectToAction("AdminProfile", "Admin");
            return View(await _profileService.GetProfile(user.Id));
        }

        public async Task<IActionResult> UserProfileSettings()
        {
            Before();
            ViewData["Message"] = "User profile settings page.";
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            try
            {
                return View(await _userService.GetUserSettings(user.Id));
            }
            catch (Exception e)
            {
                return View(new UserSettings(null, null, new DateTime(), null, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileSettings(UserSettings settings)
        {
            Before();
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Message"] = "User profile settings page.";
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserSettings(user, settings);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                } else {
                    ModelState.AddModelError(string.Empty, "Не удалось обновить данные");
                    return View(settings);
                }
            }
            else return View(new UserSettings(null, null, new DateTime(), null, null));
        }

        public async Task<IActionResult> AddComment(long id, string comment)
        {
            Before();
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction("SignIn", "Account");
            } else {
                _commentService.SaveComment(user, comment, id);
                return RedirectToAction("News", "Home",new {Id = id});
                //_commentService.SaveComment(user, request.Comment, request.Id);
                //return RedirectToAction("News", "Home", request.Id);
            }
        }

        public async Task<IActionResult> DeleteComment(long id)
        {
            Before();
            var user  = await _userManager.GetUserAsync(HttpContext.User);
            if(user == null)
            {
                return RedirectToAction("SignIn", "Account");
            } else {
                _commentService.DeleteComment(id, user.Id);
                return RedirectToAction("UserProfile", "User");
            }
        }
        
        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }

    }
}