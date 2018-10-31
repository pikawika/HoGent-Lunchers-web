using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;

namespace Lunchers.Models.Repositories
{
    public interface IGebruikerRepository
    {
        Gebruiker Authenticate(string username, string password);
    }
}
 