using KriptoFeet.News.Models;
using System.Collections.Generic;

namespace KriptoFeet.News.DB
{
    public interface INewsProvider
    {
        void AddNewsDB(NewsDB news);
        void UpdateNewsDB(long newsId, NewsDB news);
        void DeleteNewsDB(long newsId);
        NewsDB GetNewsDB(long newsId);
        List<NewsDB> GetNewsDB();
    }
}