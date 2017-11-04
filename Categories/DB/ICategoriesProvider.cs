using KriptoFeet.Categories.Models;
using System.Collections.Generic;

namespace KriptoFeet.Categories.DB
{
    public interface ICategoriesProvider
    {
        void AddCategory(CategoryDB category);
        void UpdateCategory(long categoryId, CategoryDB category);
        void DeleteCategory(long categoryId);
        CategoryDB GetCategory(long categoryId);
        List<CategoryDB> GetCategories();
    }
}