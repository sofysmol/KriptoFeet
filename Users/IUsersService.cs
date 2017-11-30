using KriptoFeet.Users.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace KriptoFeet.Users
{
    public interface IUsersService
    {
        Task<AuthorInfo> GetAuthor(string id);
        //void CreateUser(User user);
        Task<bool> UpdateUserSettings(Account user, UserSettings settings);
        Task<UserSettings> GetUserSettings(string id);
    }
}