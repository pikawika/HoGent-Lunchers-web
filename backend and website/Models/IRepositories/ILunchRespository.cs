using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;

namespace Lunchers.Models.Repositories
{
    public interface ILunchRespository
    {
        IEnumerable<Lunch> GetAll();
        IEnumerable<Lunch> GetAllFromLocation(double latitude, double longitude);
        Lunch GetById(int id);
        void Add(Lunch lunch);
        void Delete(int id);
        void SaveChanges();
    }
}
 