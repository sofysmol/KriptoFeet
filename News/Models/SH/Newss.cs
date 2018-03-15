using System;
namespace KriptoFeet.News.Models.SH
{
    public class Newss
    {
     public Newss(long id,
                    long categoryId,
                    long authorId,
                    DateTime date,
                    string title,
                    string body)
        {
            Id = id;
            CategoryId = categoryId;
            AuthorId = authorId;
            Date = date;
            Title = title;
            Body = body;
        }
        public long Id { get; set; }

        public long CategoryId { get; set; }
        public long AuthorId { get; set; }
        public DateTime Date { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
    }
}