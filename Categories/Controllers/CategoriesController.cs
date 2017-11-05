using System.Collections.Generic;
using KriptoFeet;
using KriptoFeet.Categories.Models;
using KriptoFeet.DB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KriptoFeet.Categories.DB;

namespace KriptoFeet.Categories.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController
    {
        private readonly ICategoriesProvider _categoriesProvider;

        public CategoriesController(ICategoriesProvider categoriesProvider)
        {
            _categoriesProvider = categoriesProvider;
        }

        [HttpGet]
        public IEnumerable<CategoryDB> Get()
        {
            return _categoriesProvider.GetCategories();
        }

        [HttpGet("{id}")]
        public CategoryDB Get(long id)
        {
            return _categoriesProvider.GetCategory(id);
        }

        [HttpPost]
        public void Post([FromBody]CategoryDB value)
        {
            _categoriesProvider.AddCategory(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]CategoryDB value)
        {
            _categoriesProvider.UpdateCategory(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _categoriesProvider.DeleteCategory(id);
        }
        
    }
}