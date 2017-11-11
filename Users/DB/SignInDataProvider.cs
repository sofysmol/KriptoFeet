using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using KriptoFeet.Users.Models;
using KriptoFeet.DB;

namespace KriptoFeet.Users.DB
{
    public class SignInDataProvider : ISignInDataProvider
    {
        private readonly DomainModelMySqlContext _context;
        private readonly ILogger _logger;

        public SignInDataProvider(DomainModelMySqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("SignInDataProvider");
        }
        public void AddSignInData(SignInData user)
        {
            _context.SignInData.Add(user);
            _context.SaveChanges();

        }
        public void UpdateSignInData(SignInData user)
        {
            _context.SignInData.Update(user);
            _context.SaveChanges();
        }

        public void DeleteSignInData(string email)
        {
            var entity = _context.SignInData.First(t => t.Email == email);
            _context.SignInData.Remove(entity);
            _context.SaveChanges();
        }
        public SignInData GetSignInData(string email)
        {
            return _context.SignInData.First(t => t.Email == email);
        }
    }
}