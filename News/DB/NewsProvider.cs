using KriptoFeet.News.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace KriptoFeet.News.DB
{
    public class NewsProvider : INewsProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public NewsProvider(DomainModelMySqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("NewsProvider");;
        }
        public void AddNewsDB(NewsDB news)
        {
            _context.News.Add(news);
            _context.SaveChanges();
        }
        public void UpdateNewsDB(long newsId, NewsDB news)
        {
            _context.News.Update(news);
            _context.SaveChanges();
        }
        public void DeleteNewsDB(long newsId)
        {
            var entity = _context.News.First(t => t.Id == newsId);
            _context.News.Remove(entity);
            _context.SaveChanges();
        }
        public NewsDB GetNewsDB(long newsId)
        {
            return _context.News.First(t => t.Id == newsId);
        }
        public List<NewsDB> GetNewsDB()
        {
            return _context.News.ToList();
        }
    }
}