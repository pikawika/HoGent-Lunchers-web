using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class FavorietRepository : IFavorietRepository
    {
        private readonly DbSet<Favoriet> _favorieten;
        private readonly ApplicationDbContext _context;

        public FavorietRepository(ApplicationDbContext context)
        {
            _context = context;
            _favorieten = context.Favorieten;
        }

        public void Add(Favoriet favoriet)
        {
            _favorieten.Add(favoriet);
        }

        public void Delete(Favoriet favoriet)
        {
            _favorieten.Remove(favoriet);
        }

        public IEnumerable<Favoriet> GetAll()
        {
            return _favorieten.Include(f => f.Lunch).ToList();
        }

        public IEnumerable<Favoriet> GetAllFromCustomer(int customerId)
        {
            return _favorieten.Where(f => f.Klant.GebruikerId == customerId)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .ToList();
        }

        public Favoriet GetById(int id)
        {
            return _favorieten.Include(r => r.Klant)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .SingleOrDefault(r => r.FavorietId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
