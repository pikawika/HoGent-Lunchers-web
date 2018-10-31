using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

namespace Lunchers.Data.Repositories
{
    public class LunchRespository : ILunchRespository
    {
        private readonly DbSet<Lunch> _lunches;
        private readonly ApplicationDbContext _context;

        public LunchRespository(ApplicationDbContext context)
        {
            _context = context;
            _lunches = context.Lunches;
        }

        public IEnumerable<Lunch> GetAll()
        {
            return _lunches.Include(l => l.Afbeeldingen).Include(l => l.Tags).ThenInclude(t => t.Tag).Include(l => l.Ingredienten).ThenInclude(i => i.Ingredient).ToList();
        }
    }
}
