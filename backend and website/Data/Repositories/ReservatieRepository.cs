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
            return _reservaties.Include(r => r.Lunch)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .Include(r => r.Lunch).ThenInclude(l => l.Handelaar).ThenInclude(li => li.Locatie)
                .Include(r => r.Klant)
                .OrderByDescending(r => r.Datum)
                .ToList();
        }

        public IEnumerable<Reservatie> GetAllFromMerchant(int merchantId)
        {
            return _reservaties.Where(r => r.Lunch.Handelaar.GebruikerId == merchantId)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .Include(r => r.Lunch).ThenInclude(l => l.Handelaar).ThenInclude(li => li.Locatie)
                .ToList();
        }

        public IEnumerable<Reservatie> GetAllFromCustomer(int customerId)
        {
            return _reservaties.Where(r => r.Klant.GebruikerId == customerId)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .Include(r => r.Lunch).ThenInclude(l => l.Handelaar).ThenInclude(li => li.Locatie)
                .ToList();
        }

        public Reservatie GetById(int id)
        {
            return _reservaties.Include(r => r.Klant)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(r => r.Lunch).ThenInclude(l => l.LunchTags).ThenInclude(li => li.Tag)
                .Include(r => r.Lunch).ThenInclude(l => l.Afbeeldingen)
                .Include(r => r.Lunch).ThenInclude(l => l.Handelaar).ThenInclude(li => li.Locatie)
                .SingleOrDefault(r => r.ReservatieId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
