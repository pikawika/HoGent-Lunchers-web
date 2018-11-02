using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly DbSet<Rol> _rollen;
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
            _rollen = context.Rollen;
        }

        public Rol GetByNaam(string naam)
        {
            return _rollen.FirstOrDefault(r => r.Naam == naam);
        }
    }
}
