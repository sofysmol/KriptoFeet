using KriptoFeet.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Comments.Models
{
    public class Comment
    {
        public Comment(long id, string body, AuthorInfo author, DateTime date)
        {
            Id = id;
            Body =body;
            Author = author;
            Date = date;
        }

        public long Id { get; set; }
        public string Body { get; set; }
        public AuthorInfo Author { get; set; }

        public DateTime Date { get; set; }
    }
}