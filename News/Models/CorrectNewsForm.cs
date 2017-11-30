using KriptoFeet.Categories.Models;
using KriptoFeet.Comments.Models;
using KriptoFeet.Users.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace KriptoFeet.News.Models
{
    public class CorrectNewsForm
    {
        public CorrectNewsForm(long id, string title, string body, long categoryId, DateTime date, List<Comment> comments)
        {
            Id = id;
            Title = title;
            Body = body;
            CategoryId = categoryId;
            Date = date;
            Comments = comments;
        }

        public CorrectNewsForm()
        {
        }
        public long Id {get;set;}
        
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Дата")]
        public DateTime Date {get; set;}

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Заголовок")]
        public string Title  {get; set;}

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Текс")]
        public string Body  {get; set;}

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Новостная группа")]
        public long CategoryId  {get; set;}
        public List<Comment> Comments {get; set;}
    }
}