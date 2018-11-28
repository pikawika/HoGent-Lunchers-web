using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IReservatieRepository
    {
        IEnumerable<Reservatie> GetAll();
        IEnumerable<Reservatie> GetAllFromMerchant(int merchantId);
        IEnumerable<Reservatie> GetAllFromCustomer(int customerId);
        Reservatie GetById(int id);
        void Add(Reservatie reservatie);
        void SaveChanges();
    }
}
