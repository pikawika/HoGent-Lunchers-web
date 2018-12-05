using System;
using System.Collections.Generic;
using System.Linq;
using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Lunchers.Data.Repositories
{
    public class AllergieRepository : IAllergieRepository
    {
        private readonly DbSet<Allergie> _allergie;
        private readonly ApplicationDbContext _context;

        public AllergieRepository(ApplicationDbContext context)
        {
            _context = context;
            _allergie = context.Allergies;
        }

        public void Add(Allergie allergie)
        {
            _allergie.Add(allergie);
        }

        public void Delete(Allergie allergie)
        {
            _allergie.Remove(allergie);
        }

        public IEnumerable<Allergie> GetAll()
        {
            return _allergie.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
