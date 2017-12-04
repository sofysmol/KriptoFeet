using KriptoFeet.Users.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using KriptoFeet.Exceptions;
using KriptoFeet.Utils;
using KriptoFeet.Comments;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using KriptoFeet.Utils;

namespace KriptoFeet.Users
{

    public class UsersService : IUsersService
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILongRandomGenerator rand;
        private readonly ILogger _logger;

        private readonly UserManager<Account> _userManager;
        public UsersService(UserManager<Account> userManager,
                            ILongRandomGenerator randomGenerator,
                            IHostingEnvironment hostingEnvironment,
                            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger("NewsService");
            _hostingEnvironment = hostingEnvironment;
            rand = randomGenerator;
        }

        public async Task<AuthorInfo> GetAuthor(string id)
        {
            Account user = await _userManager.FindByIdAsync(id);
            /*if (user == null)
            {
                return NotFound();
            }*/
            return new AuthorInfo(id, user.UserName, user.AvatarId);
        }

        /*public void CreateUser(User user)
        {
            List<Account> users = await _userManager.Users.Where(u => u.Nickname == user.Nickname || u.Email ==user.Email).ToList();
            if (users.Count > 0 && users.First().Nickname == user.Nickname) throw new NicknameException();
            if (users.Count > 0 && users.First().Email == user.Email) throw new EmailException();
        
            UserDB userDB = new UserDB(rand.Next(), user.FirstName, user.LastName, user.Birthday,user.Nickname, user.Email); 
            _usersProvider.AddUser(userDB);
            _signInDataProvider.AddSignInData(new SignInData(user.Email, user.Password));
        }*/

        public async Task<bool> UpdateUserSettings(Account user, UserSettings settings)
        {
            if (user != null)
            {
                user.Birthday = settings.Birthday;
                user.Email = settings.Email;
                user.FirstName = settings.FirstName;
                user.LastName = settings.LastName;
                user.UserName = settings.Nickname;
                var result = await _userManager.UpdateAsync(user);
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "images", "avatars", user.AvatarId.ToString());
                if (settings.AvatarImage != null && settings.AvatarImage.Length > 0 && ImageUtils.IsImage(settings.AvatarImage))
                {
                    using (var stream = new FileStream(path, FileMode.OpenOrCreate))
                    {
                        await settings.AvatarImage.CopyToAsync(stream);
                    }
                }
                return result.Succeeded;
            }
            return false;

        }

        public async Task<UserSettings> GetUserSettings(string id)
        {
            Account user = await _userManager.FindByIdAsync(id);
            /*if (user == null)
            {
                return NotFound();
            }*/
            return new UserSettings(user.FirstName, user.LastName, user.Birthday, user.UserName, user.Email, null, user.AvatarId);
        }
    }
}