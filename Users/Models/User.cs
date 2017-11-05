using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace KriptoFeet.Users.Models
{
    public class User
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime Birthday {get; set;}

        public string Nickname {get; set;}
        public string Email {get; set;}
    }
}