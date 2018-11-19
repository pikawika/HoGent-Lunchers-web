using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Data.Repositories
{
    public class AfbeeldingRepository : IAfbeeldingRepository
    {
        private readonly DbSet<Afbeelding> _afbeeldingen;
        private readonly ApplicationDbContext _context;

        public AfbeeldingRepository(ApplicationDbContext context)
        {
            _context = context;
            _afbeeldingen = context.Afbeeldingen;
        }

        public void Add(Afbeelding afbeelding)
        {
            _afbeeldingen.Add(afbeelding);
        }

        public void Delete(Afbeelding afbeelding)
        {
            _afbeeldingen.Remove(afbeelding);
        }

        public IEnumerable<Afbeelding> GetAll()
        {
            return _afbeeldingen.ToList();
        }

        public Afbeelding GetById(int id)
        {
            return _afbeeldingen.SingleOrDefault(a => a.AfbeeldingId == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
