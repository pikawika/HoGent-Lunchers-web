using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.IRepositories
{
    public interface IRolRepository
    {
        Rol GetByNaam(string naam);
    }
}
