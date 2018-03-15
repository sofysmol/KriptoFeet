using KriptoFeet.News.Models.SH;

namespace KriptoFeet.Users.Models.SH
{
    public interface IAuthor
    {
         long AuthorId {get;}
         Newss writeNews(long categoryId, string title, string body);
         Newss editNews(Newss news, long categoryId, string title, string body);
         
    }
}