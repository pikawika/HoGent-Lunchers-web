using Lunchers.Models;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class ReservatieRepository : IReservatieRepository
    {
        private readonly DbSet<Reservatie> _reservaties;
        private readonly ApplicationDbContext _context;

        public ReservatieRepository(ApplicationDbContext context)
        {
            _context = context;
            _reservaties = context.Reservaties;
        }

        public void Add(Reservatie reservatie)
        {
            _reservaties.Add(reservatie);
        }

        public IEnumerable<Reservatie> GetAll()
        {
            return _reservaties.Include(r => r.Lunch).ToList();
        }

        public IEnumerable<Reservatie> GetAllFromCustomer(int customerId)
        {
            return _reservaties.Where(r => r.Klant.GebruikerId == customerId)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .ToList();
        }

        public Reservatie GetById(int id)
        {
            return _reservaties.Include(r => r.Klant)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .SingleOrDefault(r => r.ReservatieId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
