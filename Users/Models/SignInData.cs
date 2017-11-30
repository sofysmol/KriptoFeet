using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    public class SignInData
    {

        public SignInData(string nickname, string password, string returnUrl)
        {
            Nickname = nickname;
            Password = password;
            ReturnUrl = returnUrl;
        }

        public SignInData()
        {

        }

        [Key]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}