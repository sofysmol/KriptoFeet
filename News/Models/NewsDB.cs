using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.News.Models
{
    using System.ComponentModel.DataAnnotations;
    public class NewsDB
    {
        [Key]
        public long Id {get; set;}

        public long CategotyId {get; set;}
        public string AuthorId {get; set;}
        public DateTime Date {get; set;}

        public string Title {get; set;}

        [Column(TypeName = "text")]
        public string Body {get; set;}

        public byte[] Picture {get; set;}
    }
}