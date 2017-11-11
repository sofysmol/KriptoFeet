using KriptoFeet.Categories.Models;
using KriptoFeet.Comments.Models;
using KriptoFeet.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KriptoFeet.News.Models
{
    public class NewsInfo
    {
        public NewsInfo(long id,
                        CategoryDB category,
                        AuthorInfo author,
                        List<Comment> comments,
                        DateTime date,
                        string title,
                        string body)
        {
            Id = id;
            Category = category;
            Author = author;
            Date = date;
            Title = title;
            Body = body;
            Comments = comments;
        }
        public long Id { get; set; }

        public CategoryDB Category { get; set; }
        public AuthorInfo Author { get; set; }

        public List<Comment> Comments { get; set; }
        public DateTime Date { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
    }
}