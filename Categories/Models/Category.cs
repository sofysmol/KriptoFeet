using KriptoFeet.News.Models;
namespace KriptoFeet.Categories.Models
{
    public class Category
    {
        public Category(long Id, string Name, NewsDB news)
        {
            this.Id = Id;
            this.Name = Name;
            this.PopularNews = news;
        }
        public long Id {get; set;}
        public string Name {get; set;}
        public NewsDB PopularNews {get; set;}
    }
}