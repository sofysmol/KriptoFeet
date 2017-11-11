namespace KriptoFeet.Users.Models
{
    public class AuthorInfo
    {
        public AuthorInfo(long id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
        public long Id;
        public string Nickname;
    }
}