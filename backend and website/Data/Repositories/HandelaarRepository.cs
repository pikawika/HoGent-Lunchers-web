using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

namespace Lunchers.Data.Repositories
{
    public class HandelaarRepository : IHandelaarRepository
    {
        private readonly DbSet<Handelaar> _handelaars;
        private readonly ApplicationDbContext _context;

        public HandelaarRepository(ApplicationDbContext context)
        {
            _context = context;
            _handelaars = context.Handelaars;
        }

        public IEnumerable<Handelaar> GetAll()
        {
            return _handelaars.Include(h => h.Lunches).ToList();
        }
    }
}
