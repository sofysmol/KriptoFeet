using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using KriptoFeet.DB;
using KriptoFeet.Comments.Models;

namespace KriptoFeet.Comments.DB
{
    public class CommentsProvider : ICommentsProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public CommentsProvider(DomainModelMySqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("CommentsProvider");
        }

        public void AddComment(CommentDB comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public void UpdateComment(long commentId, CommentDB comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
        }

        public void DeleteComment(long commentId)
        {
            var entity = _context.Comments.First(t => t.Id == commentId);
            _context.Comments.Remove(entity);
            _context.SaveChanges();
        }

        public CommentDB GetComment(long commentId)
        {
            return _context.Comments.First(t => t.Id == commentId);
        }

        public List<CommentDB> GetComments()
        {
            return _context.Comments.ToList();
        }

        public List<CommentDB> GetCommentsByNewsId(long id)
        {
            return _context.Comments.Where(c => c.NewsId == id).ToList();
        }
    }
}