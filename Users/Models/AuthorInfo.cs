namespace KriptoFeet.Users.Models
{
    public class AuthorInfo
    {
        public AuthorInfo(string id, string nickname)
        {
            Id = id;
            Nickname = nickname;
        }
        public string Id;
        public string Nickname;
    }
}