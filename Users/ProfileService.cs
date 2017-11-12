using KriptoFeet.Users.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
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
        public ProfileService(IUsersProvider usersProvider,
                            ICommentsService commentsService,
                            ILoggerFactory loggerFactory)
        {
            _usersProvider = usersProvider;
            _commentsService = commentsService;
            _logger = loggerFactory.CreateLogger("NewsService");
        }
        public UserProfile GetProfile()
        {
            UserDB user = _usersProvider.GetUsers().FirstOrDefault();
            return new UserProfile(user.Nickname, user.Email, _commentsService.GetCommentsByAuthorId(user.Id));   

        }
    }
}