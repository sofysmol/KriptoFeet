using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CreateGroupView
    {

        public CreateGroupView(string name)
        {
            Name = name;
        }

        public CreateGroupView()
        {

        }

        [Key]
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        public string Name { get; set; }
    }
}