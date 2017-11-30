using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KriptoFeet.Users.Models
{
    public class UserSettings
    {
        public UserSettings(string fiestName, string lastName, DateTime birthday, string nickname, string email)
        {
            FirstName = fiestName;
            LastName = lastName;
            Birthday = birthday;
            Nickname = nickname;
            Email = email;
        }

        public UserSettings()
        {

        }
        
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "День рождения")]
        public DateTime Birthday { get; set; }

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [Display(Name = "Ник")]
        public string Nickname { get; set; }   

        [Required (ErrorMessage = "Поле должно быть заполнено")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }     
        
    }
}