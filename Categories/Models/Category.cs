using KriptoFeet.News.Models;
namespace KriptoFeet.Categories.Models
{
    public class Category
    {
        public Category(string Name, NewsDB news)
        {
            this.Name = Name;
            this.PopularNews = news;
        }
        public string Name {get; set;}
        public NewsDB PopularNews {get; set;}
    }
}