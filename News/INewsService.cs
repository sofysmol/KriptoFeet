using KriptoFeet.News.Models;
namespace KriptoFeet.News
{
    public interface INewsService
    {
         NewsInfo GetNews(long id);
    }
}