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

namespace KriptoFeet.Users
{
    
    public class UsersService : IUsersService
    {
        private readonly ILogger _logger;

        private readonly ICommentsProvider _commentsProvider;

        private readonly IUsersProvider _usersProvider;
        public UsersService(IUsersProvider usersProvider, ICommentsProvider commentsProvider, ILoggerFactory loggerFactory)
        {
            _usersProvider = usersProvider;
            _commentsProvider = commentsProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
        }

        public AuthorInfo GetAuthor(long id)
        {
            UserDB user = _usersProvider.GetUser(id);
            return new AuthorInfo(id, user.Nickname);
        }

        public void CreateUser(User user)
        {
            /*if(!user.Agreement) throw new AgreementException();
            if(user.Birthday == null || user.Email == null || user.FirstName == null 
            || user.LastName == null || user.Nickname == null)*/
        }
    }
}