using KriptoFeet.Users.Models;
using KriptoFeet.Comments.Models;
using System.Collections.Generic;
using KriptoFeet.DB;
using Microsoft.Extensions.Logging;
using System.Linq;
using KriptoFeet.Comments.DB;
using System;
using MoreLinq;
using KriptoFeet.Users.DB;
using KriptoFeet.Users;

namespace KriptoFeet.Comments
{
    public interface ICommentsService
    {
         List<Comment> GetCommenstsByNewsId(long id);
    }
}