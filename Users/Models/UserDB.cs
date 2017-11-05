using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserDB
    {
        [Key]
        public long Id {get; set;}

        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime Birthday {get; set;}

        public string Nickname {get; set;}
        public string Email {get; set;}

    }
}