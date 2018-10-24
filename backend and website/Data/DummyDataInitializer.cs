using System;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;

namespace Lunchers.Data
{
    public class DummyDataInitializer
    {

        private readonly ApplicationDbContext _dbContext;

        public DummyDataInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                Rol rolAdmin = new Rol { Naam = "Admin" };
                Rol rolStandaard = new Rol { Naam = "Standaard" };
                _dbContext.Rollen.Add(rolAdmin);
                _dbContext.Rollen.Add(rolStandaard);
                Gebruiker gebruiker1 = new Gebruiker { Voornaam = "Team", Achternaam = "GDPR", Gebruikersnaam = "teamGDPR", Email = "teamgdpr@qarfa.be", Wachtwoord = "wachtwoord123", Telefoonnummer = "0491234514", Rol = rolAdmin };
                Gebruiker gebruiker2 = new Gebruiker { Voornaam = "Een", Achternaam = "Klant", Gebruikersnaam = "klant", Email = "klant@qarfa.be", Wachtwoord = "wachtwoord123", Telefoonnummer = "0491234514", Rol = rolStandaard };
                _dbContext.Gebruikers.Add(gebruiker1);
                _dbContext.Gebruikers.Add(gebruiker2);
                _dbContext.SaveChanges();
            }
        }
    }
}
