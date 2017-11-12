using System;
using KriptoFeet.Users.Models;
using System.Collections.Generic;

namespace KriptoFeet.Users.DB
{
    public interface IUsersProvider
    {
        void AddUser(UserDB user);
        void UpdateUser(UserDB user);
        void DeleteUser(long userId);
        UserDB GetUser(long userId);
        List<UserDB> GetUsers(); 
        UserDB GetUserByEmail(string email);  
    }
}