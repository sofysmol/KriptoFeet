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
        public long authorId {get;set;}
        public long newsId {get; set;}

        [Column(TypeName = "text")]
        public string comment {get; set;}

    }
}