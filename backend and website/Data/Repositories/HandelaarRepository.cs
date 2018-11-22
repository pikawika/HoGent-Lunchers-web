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
            IEnumerable<Handelaar> handelaarsMetAlleLunches = _handelaars.Where(h => h.Login.Rol.Naam == "handelaar")
                .Include(h => h.Locatie)
                .Include(h => h.Lunches.Where(l => l.Deleted == false)).ThenInclude(l => l.Afbeeldingen)
                .Include(h => h.Lunches.Where(l => l.Deleted == false)).ThenInclude(t => t.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(h => h.Lunches.Where(l => l.Deleted == false)).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient);

            IEnumerable<Handelaar> handelaarEnkelLunchesGeldig = handelaarsMetAlleLunches;

            if (handelaarEnkelLunchesGeldig != null) {
                foreach (Handelaar h in handelaarEnkelLunchesGeldig)
                h.Lunches.ToList().RemoveAll(l => l.EindDatum <= DateTime.Now.Date || l.BeginDatum >= DateTime.Now.Date);
            }

            return handelaarEnkelLunchesGeldig;
        }

        public Handelaar GetById(int id)
        {
            Handelaar handelaarMetAlleLunches = _handelaars.Where(h => h.Login.Rol.Naam == "handelaar" && h.GebruikerId == id)
                .Include(h => h.Locatie)
                .Include(h => h.Lunches).ThenInclude(l => l.Afbeeldingen)
                .Include(h => h.Lunches).ThenInclude(t => t.LunchTags).ThenInclude(lt => lt.Tag)
                .Include(h => h.Lunches).ThenInclude(l => l.LunchIngredienten).ThenInclude(li => li.Ingredient)
                .FirstOrDefault();

            Handelaar handelaarEnkelLunchesGeldig = handelaarMetAlleLunches;

            if (handelaarEnkelLunchesGeldig != null) handelaarEnkelLunchesGeldig.Lunches.ToList().RemoveAll(l => l.EindDatum <= DateTime.Now.Date || l.BeginDatum >= DateTime.Now.Date);

            return handelaarEnkelLunchesGeldig;
        }

        public void Add(Handelaar handelaar)
        {
            _handelaars.Add(handelaar);
        }

        public void Delete(Handelaar handelaar)
        {
            _handelaars.Remove(handelaar);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
