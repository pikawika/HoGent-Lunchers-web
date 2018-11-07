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
            return _lunches.Where(l => l.BeginDatum <= DateTime.Now.Date && l.EindDatum >= DateTime.Now.Date )
                .Include(l => l.Afbeeldingen)
                .Include(l => l.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(l => l.Handelaar).ThenInclude(h => h.Locatie)
                .ToList();
        }

        public Lunch getById(int id)
        {
            return _lunches.Where(l => l.LunchId == id)
                .Include(l => l.Afbeeldingen)
                .Include(l => l.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(l => l.Handelaar).ThenInclude(h => h.Locatie)
                .FirstOrDefault();
        }
    }
}
