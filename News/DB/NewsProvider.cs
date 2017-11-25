using KriptoFeet.News.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using Microsoft.EntityFrameworkCore;

namespace KriptoFeet.News.DB
{
    public class NewsProvider : INewsProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        private readonly ICommentsProvider _commentsProvider;

        public NewsProvider(DomainModelMySqlContext context,
                            ILoggerFactory loggerFactory,
                            ICommentsProvider commentsProvider)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("NewsProvider");
            _commentsProvider = commentsProvider;
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
            var entity = _context.News.FirstOrDefault(t => t.Id == newsId);
            _context.News.Remove(entity);
            _context.SaveChanges();
        }
        public NewsDB GetNewsDB(long newsId)
        {
            return _context.News.FirstOrDefault(t => t.Id == newsId);
        }
        public List<NewsDB> GetNewsDB()
        {
            DbSet<NewsDB> news = _context.News;
            if (news != null)
                return _context.News.ToList();
            else return new List<NewsDB>();
        }

        public List<NewsDB> GetNewsDBByCategory(long id)
        {
            return GetNewsDB().Where(n => n.CategotyId == id).ToList();
        }

        public List<NewsDB> GetNewsDBByAuthor(long id)
        {
            return GetNewsDB().Where(n => n.AuthorId == id).ToList();
        }
        public NewsDB GetPopularNewsForCategory(long id)
        {
            try
            {
                return GetNewsDB().Where(n => n.CategotyId == id).Select((n, i) =>
                    Tuple.Create(n, _commentsProvider.GetCommentsByNewsId(n.Id).Count)
                ).ToList().MaxBy(x => x.Item2).Item1;
            }
            catch (Exception e)
            {
                _logger.LogError("Can't get PopularNews", e);
                return new NewsDB();
            }
        }

        public NewsDB GetLastNews()
        {
            try
            {
                return GetNewsDB().MaxBy(x => x.Date);
            }
            catch (Exception e)
            {
                _logger.LogError("Can't get LastNews", e);
                return new NewsDB();
            }
        }
    }
}