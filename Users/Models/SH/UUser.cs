using System;
using KriptoFeet.Comments.Models;
using KriptoFeet.Exceptions;
namespace KriptoFeet.Users.Models.SH
{
    public class UUser: IUser
    {
        public long Id {get;}

        public string FirstName {get;set;}
        public string LastName {get; set;}
        public DateTime Birthday {get;set;}

        public string Nickname {get;set;}
        public string Email {get;set;}
        public long AvatarId {get;set;}
        public string Password {get;set;}
        public Boolean RequestContentManagerRights {get; set;}

        public UUser(long id, string firstName, string lastName, 
        DateTime birthday, string nickname, string email, long avatarId, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Nickname = nickname;
            Email = email;
            AvatarId = avatarId;
            Password = password;
        }
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

    }
}