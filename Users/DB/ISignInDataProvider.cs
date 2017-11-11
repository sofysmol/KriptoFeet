using KriptoFeet.Users.Models;
using System.Collections.Generic;
namespace KriptoFeet.Users.DB
{
    public interface ISignInDataProvider
    {
        void AddSignInData(SignInData user);
        void UpdateSignInData(SignInData user);
        void DeleteSignInData(string email);
        SignInData GetSignInData(string email); 
    }
}