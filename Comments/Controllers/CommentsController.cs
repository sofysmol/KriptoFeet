using System.Collections.Generic;
using KriptoFeet;
using KriptoFeet.Comments.DB;
using KriptoFeet.DB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KriptoFeet.Comments.Models;

namespace KriptoFeet.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly ICommentsProvider _commentsProvider;

        public CommentsController(ICommentsProvider dataAccessProvider)
        {
            _commentsProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<CommentDB> Get()
        {
            return _commentsProvider.GetComments();
        }

        [HttpGet("{id}")]
        public CommentDB Get(long id)
        {
            return _commentsProvider.GetComment(id);
        }

        [HttpPost]
        public void Post([FromBody]CommentDB value)
        {
            _commentsProvider.AddComment(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]CommentDB value)
        {
            _commentsProvider.UpdateComment(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _commentsProvider.DeleteComment(id);
        }
    }
}