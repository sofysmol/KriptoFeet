using System.Collections.Generic;
using KriptoFeet;
using KriptoFeet.Comments.DB;
using KriptoFeet.DB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KriptoFeet.Comments.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using KriptoFeet.Users.Models;
using System;

namespace KriptoFeet.Controllers
{
    [Route("Images/")]
    public class ImagesController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<Account> _userManager;
        

        public ImagesController(IHostingEnvironment hostingEnvironment,
                                UserManager<Account> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        [HttpGet("Avatar/{id}")]
        public async Task<IActionResult> Avatar(string id)
        {
            try {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "images", "avatars", id);
                var avatar = System.IO.File.OpenRead(path); 
                return File(avatar, "image/jpeg");
            } catch(Exception e)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "UserPhoto.png");
                var avatar = System.IO.File.OpenRead(path);
                return File(avatar, "image/jpeg");
            }
        }

        [HttpGet("News/{id}")]
        public async Task<IActionResult> News(string id)
        {
            try {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "images", "news", id);
                var avatar = System.IO.File.OpenRead(path); 
                return File(avatar, "image/jpeg");
            } catch(Exception e)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "images", "last-news3.png");
                var image = System.IO.File.OpenRead(path);
                return File(image, "image/png");
            }
        }
    }
}