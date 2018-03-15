using System;
using KriptoFeet.Comments.Models;
using KriptoFeet.Exceptions;
using KriptoFeet.News.Models.SH;
using KriptoFeet.Categories.Models.SH;
namespace KriptoFeet.Users.Models.SH
{
    public class Administrator: IUser, IAuthor
    {
        public long Id {get;}
        public long AuthorId {get;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime Birthday {get; set;}

        public string Nickname {get; set;}
        public string Email {get; set;}
        public long AvatarId {get; set;}
        public string Password {get; set;}

        public Comment createComment(string title, string content, long newsId)
        {
            var authorInfo = new AuthorInfo(Id.ToString(), Nickname, AvatarId);
            return new Comment(Next(), content, authorInfo, DateTime.UtcNow, newsId, title);
        }
        private Random rand = new Random();
        private long Next()
        {
            long min = 0;
            long max = 100000000000000000;
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);
            return (Math.Abs(longRand % (max - min)) + min);
        }

        public void editPersonalSettnings(string firstName, string lastName, DateTime birthday, string nickname, string email, long avatarId)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Nickname = nickname;
            Email = email;
            AvatarId = avatarId;
        }
        public void updateAvatar(long avatarId)
        {
            AvatarId = avatarId;
        }
        public void changePassword(String oldPassword, String newPassword)
        {
            if(oldPassword == Password)
            Password = newPassword;
            else throw new PasswordException();

        }
                public Newss writeNews(long categoryId, string title, string body)
        {
            return new Newss(Next(), categoryId, AuthorId, DateTime.UtcNow, title, body);
        }
        public Newss editNews(Newss news, long categoryId, string title, string body)
        {
            return new Newss(news.Id, categoryId, news.AuthorId, news.Date, title, body);
        }
        public Category createCategory(string name)
        {
            return new Category(Next(), name);
        }

        public ContentManager giveContentManagerRights(UUser user)
        {
            if(user.RequestContentManagerRights)
            {
                return new ContentManager(user.Id, Next(), user.FirstName, user.LastName, user.Birthday,
                 user.Nickname, user.Email, user.AvatarId, user.Password);
            } else throw new AccessException();
        }

        public UUser removeContentManagerRights(ContentManager user)
        {
                return new UUser(user.Id, user.FirstName, user.LastName, user.Birthday,
                 user.Nickname, user.Email, user.AvatarId, user.Password);
        }

        public Category editCategory(Category category, string name)
        {
            return new Category(category.Id, name);
        }
    }
}