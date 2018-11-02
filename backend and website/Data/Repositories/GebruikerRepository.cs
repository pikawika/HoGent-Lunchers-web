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

        public byte[] getSalt(string gebruikersnaam)
        {
            return _gebruikers.FirstOrDefault(g => g.Login.Gebruikersnaam == gebruikersnaam).Login.Salt;
        }

        public void Registreer(Gebruiker gebruiker)
        {
            _gebruikers.Add(gebruiker);
            saveChanges();
        }

        public void WijzigWachtwoord(int gebruikersId, byte[] nieuweSalt, string nieuweHash)
        {
            Gebruiker gebruiker = _gebruikers.FirstOrDefault(g => g.GebruikerId == gebruikersId);

            gebruiker.Login.Salt = nieuweSalt;
            gebruiker.Login.Hash = nieuweHash;

            saveChanges();
        }



        private void saveChanges(){
            _context.SaveChanges();
        }
    }
}
