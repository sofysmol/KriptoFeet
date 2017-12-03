using KriptoFeet.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Comments.Models
{
    public class Comment
    {
        public Comment(long id, string body, AuthorInfo author, DateTime date, long newsId, string title)
        {
            Id = id;
            Body = body;
            Author = author;
            Date = date;
            NewsId = newsId;
            Title = title;
        }

        public long Id { get; set; }
        public long NewsId { get; set; }
        public string Title {get; set;}
        public string Body { get; set; }
        public AuthorInfo Author { get; set; }

        public DateTime Date { get; set; }
    }
}