using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using KriptoFeet.Users.DB;
using KriptoFeet.Users;

namespace KriptoFeet.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ILogger _logger;

        private readonly ICommentsProvider _commentsProvider;

        private readonly IUsersService _usersService;
        public CommentsService(IUsersService usersService, ICommentsProvider commentsProvider, ILoggerFactory loggerFactory)
        {
            _usersService = usersService;
            _commentsProvider = commentsProvider;
            _logger = loggerFactory.CreateLogger("NewsService");
        }

        public List<Comment> GetCommenstsByNewsId(long id)
        {
            return _commentsProvider.GetComments().Where(c => c.NewsId == id)
            .Select(c => new Comment(c.Comment, _usersService.GetAuthor(c.AuthorId), c.Date)).ToList();
        }
        public List<Comment> GetCommentsByAuthorId(long id)
        {
            AuthorInfo author = _usersService.GetAuthor(id);
            return _commentsProvider.GetComments().Where(c => c.AuthorId == id).Select(c => new Comment(c.Comment, author, c.Date)).ToList();
        }
    }
}