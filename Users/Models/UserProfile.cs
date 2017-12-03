using KriptoFeet.Comments.Models;
using KriptoFeet.Categories.Models;
using KriptoFeet.News.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace KriptoFeet.Users.Models
{
    public class UserProfile
    {
        public UserProfile()
        {

        }

        public UserProfile(string nickname, string email, List<Comment> comments, bool isContentManagerRequest, long avatarId)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
            News = new List<NewsInfo>();
            IsContentManagerRequest = isContentManagerRequest;
            AvatarId = avatarId;
        }
        public string Nickname {get; set;}
        public string Email {get; set;}

        public bool IsContentManagerRequest {get; set;}
        public List<Comment> Comments {get; set;}
        public List<NewsInfo> News {get; set;}
        public List<CategoryDB> Categories {get; set;}
        public List<ContentManager> ContentManagers {get; set;}
        public List<ContentManager> ContentManagersRequests {get; set;}
        public long AvatarId { get; set; }
    }
}