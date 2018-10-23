using System;
using System.Collections.Generic;

namespace Lunchers.Models.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAll();
        UserModel Authenticate(string username, string password);
        void Register(UserModel user);
    }
}
 