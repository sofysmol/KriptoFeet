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

namespace KriptoFeet.Users
{
    
    public class UsersService : IUsersService
    {
        private readonly ILongRandomGenerator rand;
        private readonly ILogger _logger;

        private readonly ICommentsProvider _commentsProvider;

        private readonly ISignInDataProvider _signInDataProvider;

        private readonly IUsersProvider _usersProvider;
        public UsersService(IUsersProvider usersProvider,
                            ICommentsProvider commentsProvider,
                            ISignInDataProvider signInDataProvider,
                            ILongRandomGenerator randomGenerator,
                            ILoggerFactory loggerFactory)
        {
            _usersProvider = usersProvider;
            _commentsProvider = commentsProvider;
            _signInDataProvider = signInDataProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
             rand = randomGenerator;
        }

        public AuthorInfo GetAuthor(long id)
        {
            UserDB user = _usersProvider.GetUser(id);
            return new AuthorInfo(id, user.Nickname);
        }

        public void CreateUser(User user)
        {
            List<UserDB> users = _usersProvider.GetUsers().Where(u => u.Nickname == user.Nickname || u.Email ==user.Email).ToList();
            if (users.Count > 0 && users.First().Nickname == user.Nickname) throw new NicknameException();
            if (users.Count > 0 && users.First().Email == user.Email) throw new EmailException();
        
            UserDB userDB = new UserDB(rand.Next(), user.FirstName, user.LastName, user.Birthday,user.Nickname, user.Email); 
            _usersProvider.AddUser(userDB);
            _signInDataProvider.AddSignInData(new SignInData(user.Email, user.Password));
        }
    }
}