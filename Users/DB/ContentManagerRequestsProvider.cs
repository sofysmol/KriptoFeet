using KriptoFeet.Users.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using Microsoft.EntityFrameworkCore;
namespace KriptoFeet.Users.DB
{
    public class ContentManagerRequestsProvider : IContentManagerRequestProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public ContentManagerRequestsProvider(DomainModelMySqlContext context,
                                            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("ContentManagerRequestsProvider");
        }
        public void AddRequest(ContentManagerRequest request)
        {
            _context.ContentManagerRequests.Add(request);
            _context.SaveChanges();
        }
        public void DeleteRequest(string id)
        {
            var entity = _context.ContentManagerRequests.FirstOrDefault(t => t.Id == id);
            _context.ContentManagerRequests.Remove(entity);
            _context.SaveChanges();
        }

        public List<ContentManagerRequest> GetList()
        {
            return _context.ContentManagerRequests.ToList();
        }
         public ContentManagerRequest GetRequest(string id)
         {
             return _context.ContentManagerRequests.FirstOrDefault(t => t.Id == id);
         }
    }
}