using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KriptoFeet.Users.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserDB
    {
        public UserDB()
        {
            
        }
        public UserDB(long id, string firstName, string lastName, DateTime birthday, string nickname, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            Email = email;
            Birthday = birthday;
        }

        [Key]
        public long Id {get; set;}

        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime Birthday {get; set;}

        public string Nickname {get; set;}
        public string Email {get; set;}

    }
}