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
    public class AccountController : Controller
    {
        private readonly INewsProvider _newsProvider;
        private readonly ICategoriesProvider _categoriesProvider;

        private readonly INewsService _newsService;
        private readonly IUsersService _userService;

        private readonly IProfileService _profileService;

        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;

        private readonly ILongRandomGenerator _rand;

        public AccountController(INewsProvider newsProvider,
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

        public ActionResult CreateUser()
        {
            Before();
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(User model)
        {
            Before();
            if (!model.Agreement)
            {
                ModelState.AddModelError("Agreement", "Без согласия невозможно выполнить регистрацию");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                Account user = new Account
                {
                    UserName = model.Nickname,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Birthday = model.Birthday,
                    AvatarId = _rand.Next()
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                /*try
                {
                    _userService.CreateUser(User);
                    return RedirectToAction("Index", "Home");
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
                }*/
            }
            return View(model);
        }

        public ActionResult SignIn(string returnUrl = null)
        {
            Before();
            return View(new SignInData { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInData model)
        {
            Before();
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Nickname, model.Password, true, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private void Before()
        {
            ViewBag.Categories = _categoriesProvider.GetCategories();
        }
    }

}