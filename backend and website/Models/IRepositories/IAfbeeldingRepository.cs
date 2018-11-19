using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IAfbeeldingRepository
    {
        IEnumerable<Afbeelding> GetAll();
        Afbeelding GetById(int id);
        void Add(Afbeelding afbeelding);
        void Delete(Afbeelding afbeelding);
        void SaveChanges();
    }
}
