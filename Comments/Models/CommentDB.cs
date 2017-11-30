using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Comments.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CommentDB
    {
        [Key]
        public long Id {get; set;}
        public string AuthorId {get;set;}
        public long NewsId {get; set;}

        [Column(TypeName = "text")]
        public string Comment {get; set;}
        
        public DateTime Date {get; set;}
    }
}