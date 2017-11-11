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
        private readonly IUsersProvider _usersProvider;
        
        public UsersController(IUsersProvider usersProvider)
        {
            _usersProvider = usersProvider;
        }

         [HttpGet]
         public IEnumerable<UserDB> GetList()
        {
            return _usersProvider.GetUsers();
        }

        [HttpGet("{id}")]
        public UserDB GetUser(long id)
        {
            return _usersProvider.GetUser(id);
        }

        [HttpPost]
        public void Post([FromBody]UserDB value)
        {
            _usersProvider.AddUser(value);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody]UserDB value)
        {
            _usersProvider.UpdateUser(value);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _usersProvider.DeleteUser(id);
        }
    }
}