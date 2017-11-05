using System;
using KriptoFeet.Users.Models;
using System.Collections.Generic;

namespace KriptoFeet.Users.DB
{
    public interface IUserAccessProvider
    {
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long userId);
        User GetUser(long userId);
        List<User> GetUsers();  
    }
}