using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    public class SignInData
    {

        public SignInData(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public SignInData()
        {

        }

        [Key]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Password { get; set; }
    }
}