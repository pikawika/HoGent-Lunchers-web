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

        public Gebruiker Login(string gebruikersnaam, string hash)
        {
            return _gebruikers.Where(gebruiker => gebruiker.Login.Gebruikersnaam == gebruikersnaam && gebruiker.Login.Hash == hash && gebruiker.Login.Geactiveerd).Include(g => g.Login.Rol).FirstOrDefault();
        }

        public bool EmailExists(string email)
        {
            return _gebruikers.Any(g => g.Email == email);
        }

        public bool GebruikersnaamExists(string gebruikersnaam)
        {
            return _gebruikers.Any(g => g.Login.Gebruikersnaam == gebruikersnaam);
        }

        public byte[] GetSalt(string gebruikersnaam)
        {
            return _gebruikers.Where(g => g.Login.Gebruikersnaam == gebruikersnaam).Include(g => g.Login).FirstOrDefault().Login.Salt;
        }

        public void Registreer(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
            SaveChanges();
        }

        public void WijzigWachtwoord(int gebruikersId, byte[] nieuweSalt, string nieuweHash)
        {
            Gebruiker gebruiker = _gebruikers.Where(g => g.GebruikerId == gebruikersId).Include(g => g.Login).FirstOrDefault();

            gebruiker.Login.Salt = nieuweSalt;
            gebruiker.Login.Hash = nieuweHash;

            SaveChanges();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool GebruikerIsGeactiveerd(string gebruikersnaam)
        {
            return _gebruikers.Any(g => g.Login.Gebruikersnaam == gebruikersnaam && g.Login.Geactiveerd) ;
        }
    }
}
