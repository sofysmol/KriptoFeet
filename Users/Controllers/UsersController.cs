using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KriptoFeet.Users.Models;
using KriptoFeet.Users.DB;
using KriptoFeet;

namespace KriptoFeet.Users.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserAccessProvider _userAccessProvider;
        
        public UsersController(IUserAccessProvider userAccessProvider)
        {
            _userAccessProvider = userAccessProvider;
        }

         [HttpGet]
         public IEnumerable<UserDB> GetList()
        {
            return _userAccessProvider.GetUsers();
        }

        [HttpGet("{id}")]
        public UserDB GetUser(long id)
        {
            return _userAccessProvider.GetUser(id);
        }

        [HttpPost]
        public void Post([FromBody]UserDB value)
        {
            _userAccessProvider.AddUser(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]UserDB value)
        {
            _userAccessProvider.UpdateUser(value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _userAccessProvider.DeleteUser(id);
        }
    }
}