using KriptoFeet.News.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using KriptoFeet.News.DB;
using KriptoFeet.Comments;
using KriptoFeet.Comments.Models;
using KriptoFeet.Categories.DB;
using KriptoFeet.Categories.Models;
using KriptoFeet.Users;
using KriptoFeet.Users.Models;

namespace KriptoFeet.News
{
    public class NewsService : INewsService
    {
        private readonly ILogger _logger;

        private readonly ICommentsService _commentsService;

        private readonly INewsProvider _newsProvider;

        private readonly ICategoriesProvider _categoriesProvider;

        private readonly IUsersService _usersService;
        public NewsService(IUsersService usersService,
                            INewsProvider newsProvider,
                            ICommentsService commentsService,
                            ILoggerFactory loggerFactory,
                            ICategoriesProvider categoriesProvider)
        {
            _newsProvider = newsProvider;
            _commentsService = commentsService;
            _categoriesProvider = categoriesProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
            _usersService = usersService;
        }

        public NewsInfo GetNews(long id)
        {
            NewsDB news = _newsProvider.GetNewsDB(id);
            List<Comment> comments = _commentsService.GetCommenstsByNewsId(id);
            CategoryDB category = _categoriesProvider.GetCategory(news.CategotyId);
            AuthorInfo author = _usersService.GetAuthor(news.AuthorId);
            return new NewsInfo(news.Id, category, author, comments, news.Date, news.Title, news.Body);
        }
    }
}