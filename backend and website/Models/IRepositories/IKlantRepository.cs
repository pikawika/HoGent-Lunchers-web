using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IKlantRepository
    {
        Klant GetById(int customerId);
        void AddAllergy(int gebruikersId, string allergy);
        void RemoveAllergy(int gebruikersId, string allergy);
    }
}
