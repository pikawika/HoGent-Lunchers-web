using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IFavorietRepository
    {
        IEnumerable<Favoriet> GetAll();
        IEnumerable<Favoriet> GetAllFromCustomer(int customerId);
        Favoriet GetById(int id);
        void Add(Favoriet favoriet);
        void Delete(Favoriet favoriet);
        void SaveChanges();
    }
}
