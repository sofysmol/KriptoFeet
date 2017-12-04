using KriptoFeet.News.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KriptoFeet.News
{
    public interface INewsService
    {
         Task<NewsInfo> GetNews(long id);

         Task<NewsInfo[]> GetNewsByAuthor(string id);
         Task DeleteNews(long id);
    }
}