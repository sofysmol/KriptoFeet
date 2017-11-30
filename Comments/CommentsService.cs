using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using KriptoFeet.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using KriptoFeet.Utils;

namespace KriptoFeet.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly ILogger _logger;

        private readonly ICommentsProvider _commentsProvider;

        private readonly IUsersService _usersService;
        private readonly ILongRandomGenerator rand;
        public CommentsService(IUsersService usersService,
         ICommentsProvider commentsProvider,
         ILongRandomGenerator randomGenerator,
          ILoggerFactory loggerFactory)
        {
            _usersService = usersService;
            _commentsProvider = commentsProvider;
            rand = randomGenerator;
            _logger = loggerFactory.CreateLogger("NewsService");
        }

        public async Task<Comment[]> GetCommenstsByNewsId(long id)
        {
            return await Task.WhenAll(_commentsProvider.GetComments().Where(c => c.NewsId == id)
            .Select(async c => new Comment(c.Id, c.Comment, await _usersService.GetAuthor(c.AuthorId), c.Date)));
        }
        public async Task<List<Comment>> GetCommentsByAuthorId(string id)
        {
            AuthorInfo author = await _usersService.GetAuthor(id);
            return _commentsProvider.GetComments().Where(c => c.AuthorId == id).Select(c => new Comment(c.Id, c.Comment, author, c.Date)).ToList();
        }

        public void SaveComment(Account user, string comment, long newsId)
        {
            _commentsProvider.AddComment(new CommentDB{Id = rand.Next(), AuthorId = user.Id,NewsId = newsId, Comment = comment, Date = DateTime.UtcNow});
        }

        public void DeleteComment(long id, string authorId)
        {
            var c = _commentsProvider.GetComment(id);
            if(c != null && c.AuthorId == authorId)
            {
                _commentsProvider.DeleteComment(id);
            }
        }

        public CommentDB GetComment(long id)
        {
            return _commentsProvider.GetComment(id);
        }
    }
}