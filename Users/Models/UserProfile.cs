using KriptoFeet.Comments.Models;
using KriptoFeet.Categories.Models;
using KriptoFeet.News.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KriptoFeet.Users.Models
{
    public class UserProfile
    {
        public UserProfile()
        {

        }

        public UserProfile(string nickname, string email, List<Comment> comments, List<NewsInfo> news, List<CategoryDB> categories, List<string> contentManagers, List<string> contentManagersRequests)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
            News = news;
            Categories = categories;
            ContentManagers = contentManagers;
            ContentManagersRequests = contentManagersRequests;
        }
        public UserProfile(string nickname, string email, List<Comment> comments, List<NewsInfo> news, List<CategoryDB> categories)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
            News = news;
            Categories = categories;
        }
        public UserProfile(string nickname, string email, List<Comment> comments, List<NewsInfo> news)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
            News = news;
        }
        public UserProfile(string nickname, string email, List<Comment> comments)
        {
            Nickname = nickname;
            Email = email;
            Comments = comments;
            News = new List<NewsInfo>();
        }
        public string Nickname {get; set;}
        public string Email {get; set;}
        public List<Comment> Comments {get; set;}
        public List<NewsInfo> News {get; set;}
        public List<CategoryDB> Categories {get; set;}
        public List<string> ContentManagers {get; set;}
        public List<string> ContentManagersRequests {get; set;}
    }
}