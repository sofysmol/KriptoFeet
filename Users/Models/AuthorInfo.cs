namespace KriptoFeet.Users.Models
{
    public class AuthorInfo
    {
        public AuthorInfo(string id, string nickname, long avatarId)
        {
            Id = id;
            Nickname = nickname;
            AvatarId = avatarId;
        }
        public string Id;
        public string Nickname;
        public long AvatarId;
    }
}