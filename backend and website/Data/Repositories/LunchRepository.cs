using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using GeoCoordinatePortable;

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

        public void Add(Lunch lunch)
        {
            _lunches.Add(lunch);
        }

        public void Delete(Lunch lunch)
        {
            lunch.Deleted = true;
        }

        public IEnumerable<Lunch> GetAll()
        {
            return _lunches.Where(l => l.BeginDatum <= DateTime.Now.Date && l.EindDatum >= DateTime.Now.Date && l.Deleted == false )
                .Include(l => l.Afbeeldingen)
                .Include(l => l.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(l => l.Handelaar).ThenInclude(h => h.Locatie)
                .ToList();
        }

        public IEnumerable<Lunch> GetAllFromLocation(double latitude, double longitude)
        {
            List<Lunch> lunches = _lunches.Where(l => l.BeginDatum <= DateTime.Now.Date && l.EindDatum >= DateTime.Now.Date && l.Deleted == false)
                .Include(l => l.Afbeeldingen)
                .Include(l => l.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(l => l.Handelaar).ThenInclude(h => h.Locatie)
                .ToList();

            var coord = new GeoCoordinate(latitude, longitude);

            return lunches.OrderBy(l => l.GetCoordinate.GetDistanceTo(coord));
        }

        public Lunch GetById(int id)
        {
            return _lunches.Where(l => l.LunchId == id)
                .Include(l => l.Afbeeldingen)
                .Include(l => l.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .Include(l => l.Handelaar).ThenInclude(h => h.Locatie)
                .FirstOrDefault();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
