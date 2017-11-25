using KriptoFeet.Users.Models;
namespace KriptoFeet.Users
{
    public interface IProfileService
    {
        UserProfile GetProfile();
        UserProfile GetContentManagerProfile();
    }
}