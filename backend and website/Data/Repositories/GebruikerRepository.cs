using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

namespace Lunchers.Data.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly DbSet<Gebruiker> _gebruikers;
        private readonly ApplicationDbContext _context;

        public GebruikerRepository(ApplicationDbContext context)
        {
            _context = context;
            _gebruikers = context.Gebruikers;
        }

        public Gebruiker Authenticate(string gebruikersnaam, string wachtwoord)
        {
            throw new NotImplementedException();
            //return _gebruikers.Where(gebruiker => gebruiker.Gebruikersnaam == gebruikersnaam && gebruiker.Wachtwoord == wachtwoord).Include(g => g.Rol).FirstOrDefault();
        }

        public void Registreer(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
            saveChanges();
        }

        private void saveChanges(){
            _context.SaveChanges();
        }
    }
}
