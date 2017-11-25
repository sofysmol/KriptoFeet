using KriptoFeet.Users.Models;
using KriptoFeet.News.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using KriptoFeet.News.DB;
using System;
using MoreLinq;
using KriptoFeet.Users.DB;
using KriptoFeet.Exceptions;
using KriptoFeet.Utils;
using KriptoFeet.Comments;

namespace KriptoFeet.Users
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger _logger;

        private readonly ICommentsService _commentsService;

        private readonly IUsersProvider _usersProvider;
        private readonly INewsProvider _newsProvider;
        public ProfileService(IUsersProvider usersProvider,
                            INewsProvider newsProvider,
                            ICommentsService commentsService,
                            ILoggerFactory loggerFactory)
        {
            _usersProvider = usersProvider;
            _commentsService = commentsService;
            _newsProvider = newsProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
        }
        public UserProfile GetProfile()
        {
            UserDB user = _usersProvider.GetUsers().FirstOrDefault();
            return new UserProfile(user.Nickname, user.Email, _commentsService.GetCommentsByAuthorId(user.Id), new List<NewsDB>());   

        }
        public UserProfile GetContentManagerProfile()
        {
             UserProfile profile = GetProfile();
             profile.News = _newsProvider.GetNewsDBByAuthor(6599);
             return profile;
        }
    }
}