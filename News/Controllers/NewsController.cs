using System.Collections.Generic;
using KriptoFeet.News.Models;
using KriptoFeet.News.DB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KriptoFeet.News.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
         private readonly INewsProvider _newsProvider;
         public NewsController(INewsProvider newsProvider)
         {
             _newsProvider = newsProvider;
         }

         public IEnumerable<NewsDB> Get()
        {
            return _newsProvider.GetNewsDB();
        }

        [HttpGet("{id}")]
        public NewsDB Get(long id)
        {
            return _newsProvider.GetNewsDB(id);
        }

        [HttpPost]
        public void Post([FromBody]NewsDB value)
        {
            _newsProvider.AddNewsDB(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]NewsDB value)
        {
            _newsProvider.UpdateNewsDB(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _newsProvider.DeleteNewsDB(id);
        }

        
    }
}