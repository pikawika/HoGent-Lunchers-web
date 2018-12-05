using System;
using System.Collections.Generic;
using Lunchers.Models.Domain;

namespace Lunchers.Models.IRepositories
{
    public interface IAllergieRepository
    {
        IEnumerable<Allergie> GetAll();
        void Add(Allergie allergie);
        void Delete(Allergie allergie);
        void SaveChanges();
    }
}
