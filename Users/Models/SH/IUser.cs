using System;
using KriptoFeet.Comments.Models;
using Microsoft.AspNetCore.Http;
namespace KriptoFeet.Users.Models
{
    public interface IUser
    {
        long Id {get;}

        string FirstName {get; }
        string LastName {get;}
        DateTime Birthday {get;}

        string Nickname {get;}
        string Email {get;}
        long AvatarId {get;}
        string Password {get;}
        Comment createComment(string title, string content, long newsId);
        void editPersonalSettnings(string firstName, string lastName, DateTime birthday, string nickname, string email, long avatarId);
        void updateAvatar(long avatarId);
        void changePassword(String oldPassword, String newPassword);

    }
}