using KriptoFeet.Users.Models;
using System.Collections.Generic;

namespace KriptoFeet.Users.DB
{
    public interface IContentManagerRequestProvider
    {
         void AddRequest(ContentManagerRequest request);
         void DeleteRequest(string id);

         List<ContentManagerRequest> GetList();
         ContentManagerRequest GetRequest(string id);

    }
}