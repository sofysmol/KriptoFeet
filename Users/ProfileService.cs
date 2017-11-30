using KriptoFeet.Users.Models;
using KriptoFeet.News.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using KriptoFeet.News.DB;
using KriptoFeet.News;
using System;
using MoreLinq;
using KriptoFeet.Categories.DB;
using KriptoFeet.Exceptions;
using KriptoFeet.Utils;
using KriptoFeet.Comments;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace KriptoFeet.Users
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger _logger;

        private readonly ICommentsService _commentsService;

        private readonly UserManager<Account> _userManager;

        private readonly ICategoriesProvider _categoriesProvider;
        private readonly INewsService _newsService;
        public ProfileService(UserManager<Account> userManager,
                            INewsService newsService,
                            ICommentsService commentsService,
                            ICategoriesProvider categoriesProvider,
                            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _commentsService = commentsService;
            _newsService = newsService;
            _categoriesProvider = categoriesProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
        }
        public async Task<UserProfile> GetProfile(string id)
        {
            Account user = await _userManager.FindByIdAsync(id);
            /*if (user == null)
            {
                return NotFound();
            }*/
            return new UserProfile(user.UserName, user.Email, await _commentsService.GetCommentsByAuthorId(user.Id));   

        }
        public async Task<UserProfile> GetContentManagerProfile(string id)
        {
             UserProfile profile = await GetProfile(id);
             profile.News = (await _newsService.GetNewsByAuthor(id)).ToList();
             return profile;
        }

        public async Task<UserProfile> GetAdminProfile(string id)
        {
            UserProfile profile = await GetContentManagerProfile(id);
             profile.Categories = _categoriesProvider.GetCategories();
             profile.ContentManagers = new List<string>();//_userManager.Users.Select(u => u.LastName + u.FirstName).ToList();
             profile.ContentManagersRequests = new List<string>();//_userManager.Users.Select(u => u.LastName + u.FirstName).ToList();
             return profile;
        }
    }
}