using KriptoFeet.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Comments.Models
{
    public class Comment
    {
        public Comment(string body, AuthorInfo author, DateTime date)
        {
            Body =body;
            Author = author;
            Date = date;
        }
        public string Body { get; set; }
        public AuthorInfo Author { get; set; }

        public DateTime Date { get; set; }
    }
}