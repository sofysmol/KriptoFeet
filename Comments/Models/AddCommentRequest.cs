using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace KriptoFeet.Comments.Models
{
    public class AddCommentRequest
    {
        public long Id;
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        public string Comment;
    }
}