using Microsoft.AspNetCore.Identity;
using System;

namespace KriptoFeet.Users.Models
{
    public class Account: IdentityUser
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public DateTime Birthday {get; set;}
        public long AvatarId {get; set;}
    }
}