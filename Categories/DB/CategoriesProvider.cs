using KriptoFeet.Categories.Models;
using System.Collections.Generic;
using KriptoFeet.Categories.DB;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;

namespace KriptoFeet.Categories.DB
{
    public class CategoriesProvider : ICategoriesProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public CategoriesProvider(DomainModelMySqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("CategoriesProvider");
        }
        public void AddCategory(CategoryDB category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void UpdateCategory(long categoryId, CategoryDB category)
        {
            var category1 =_context.Categories.Find(categoryId);
            category1.Name = category.Name;
            _context.SaveChanges();
        }
        public void DeleteCategory(long categoryId)
        {
            var entity = _context.Categories.First(t => t.Id == categoryId);
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }
        public CategoryDB GetCategory(long categoryId)
        {
            return GetCategories().First(t => t.Id == categoryId);
        }
        public List<CategoryDB> GetCategories()
        {
            try{
            return _context.Categories.ToList();
            } catch(Exception e)
            {
                _logger.LogError("Can't get categories", e);
                return new List<CategoryDB>();
            }
        }
    }
}