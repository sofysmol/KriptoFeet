
using System.Collections.Generic;
using KriptoFeet.Comments.Models;

namespace KriptoFeet.Comments.DB
{
    public interface ICommentsProvider
    {
        void AddComment(CommentDB comment);
        void UpdateComment(long commentId, CommentDB commentDB);
        void DeleteComment(long commentId);
        CommentDB GetComment(long commentId);
        List<CommentDB> GetComments();  
    }
}