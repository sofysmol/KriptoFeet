using KriptoFeet.Users.Models;
namespace KriptoFeet.Users
{
    public interface IUsersService
    {
        AuthorInfo GetAuthor(long id);
    }
}