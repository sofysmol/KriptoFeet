using KriptoFeet.Users.Models;
namespace KriptoFeet.Users
{
    public interface IUsersService
    {
        AuthorInfo GetAuthor(long id);
        void CreateUser(User user);
        void UpdateUserSettings(UserSettings settings);
        UserSettings GetUserSettings();
    }
}