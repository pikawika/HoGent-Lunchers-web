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

        public Handelaar getById(int id)
        {
            Handelaar handelaarMetAlleLunches = _handelaars.Where(h => h.Login.Rol.Naam == "handelaar" && h.GebruikerId == id)
                .Include(h => h.Locatie)
                .Include(h => h.Lunches).ThenInclude(l => l.Afbeeldingen)
                .Include(h => h.Lunches).ThenInclude(l => l.Tags).ThenInclude(t => t.Tag)
                .Include(h => h.Lunches).ThenInclude(l => l.Ingredienten).ThenInclude(i => i.Ingredient)
                .FirstOrDefault();

            Handelaar handerlaarEnkelLunchesGeldig = handelaarMetAlleLunches;

            if (handerlaarEnkelLunchesGeldig != null)
                handerlaarEnkelLunchesGeldig.Lunches.RemoveAll(l => l.EindDatum <= DateTime.Now.Date || l.BeginDatum >= DateTime.Now.Date);

            return handerlaarEnkelLunchesGeldig;
        }
    }
}
