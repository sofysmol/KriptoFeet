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
using System.Threading.Tasks;

namespace KriptoFeet.Comments
{
    public interface ICommentsService
    {
         Task<Comment[]> GetCommenstsByNewsId(long id);
         Task<List<Comment>> GetCommentsByAuthorId(string id);
         void SaveComment(Account user, string comment, long newsId);
    
         void DeleteComment(long id, string authorId);
         CommentDB GetComment(long id);
    }
}