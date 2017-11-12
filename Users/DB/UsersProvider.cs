using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using KriptoFeet.Users.Models;
using KriptoFeet.DB;

namespace KriptoFeet.Users.DB
{
    public class UsersProvider : IUsersProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public UsersProvider(DomainModelMySqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("UsersProvider");
        }
        public void AddUser(UserDB user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

        }
        public void UpdateUser(UserDB user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(long userId)
        {
            var entity = _context.Users.First(t => t.Id == userId);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
        public UserDB GetUser(long userId)
        {
            return GetUsers().First(t => t.Id == userId);
        }
        public List<UserDB> GetUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError("Can't get Users", e);
                return new List<UserDB>();
            }
        }

       public UserDB GetUserByEmail(string email)
       {
           try{
           return GetUsers().First(t => t.Email == email);
           } catch(Exception e)
           {
               _logger.LogError("Can't find user by email", e);
               return null;
           }
       }

    }
}