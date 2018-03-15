namespace KriptoFeet.Categories.Models.SH
{
    public class Category
    {
        public Category(long Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }
        public long Id {get; set;}
        public string Name {get; set;}
    }
}