using KriptoFeet.Comments.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KriptoFeet.Users.Models
{
    public class UserProfile
    {
        public UserProfile()
        {

        }

        public UserProfile(string nickname, string email, List<Comment> comments)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
        }
        public string Nickname {get; set;}
        public string Email {get; set;}
        public List<Comment> Comments {get; set;}
    }
}