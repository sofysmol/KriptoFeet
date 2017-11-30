using KriptoFeet.Users.Models;
using System.Threading.Tasks;
namespace KriptoFeet.Users
{
    public interface IProfileService
    {
        Task<UserProfile> GetProfile(string id);
        Task<UserProfile> GetContentManagerProfile(string id);
        Task<UserProfile> GetAdminProfile(string id);
    }
}