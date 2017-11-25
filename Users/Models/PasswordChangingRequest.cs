using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KriptoFeet.Users.Models
{
    public class PasswordChangingRequest
    {
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordAgain {get; set;}

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        public string OldPassword {get; set;}
    }
}