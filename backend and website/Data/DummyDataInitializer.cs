using System;
using System.Collections.Generic;
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

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                // ROLLEN BEGIN
                Rol rolAdmin = new Rol { Naam = "Admin" };
                Rol rolStandaard = new Rol { Naam = "Standaard" };
                Rol rolHandelaar = new Rol { Naam = "Handelaar" };

                var rollen = new List<Rol>
                {
                    rolAdmin, rolStandaard, rolHandelaar
                };
                _dbContext.Rollen.AddRange(rollen);
                // ROLLEN EINDE

                // GEBRUIKERS BEGIN
                Gebruiker gebruikerStandaardLennert = new Klant { Voornaam = "Lennert", Achternaam = "Bontinck", Gebruikersnaam = "Pikawika", Email = "info@lennertbontinck.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234514", Rol = rolStandaard };
                Gebruiker gebruikerStandaard1 = new Klant { Voornaam = "Kathi", Achternaam = "Bramblett", Gebruikersnaam = "Kathi", Email = "bramblett@me.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard2 = new Klant { Voornaam = "Liza", Achternaam = "Imboden", Gebruikersnaam = "Liza", Email = "liza@optonline.net", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard3 = new Klant { Voornaam = "Christine", Achternaam = "Heisler", Gebruikersnaam = "Christine", Email = "christine@msn.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard4 = new Klant { Voornaam = "Jena", Achternaam = "Ocampo", Gebruikersnaam = "Jena", Email = "jena@sbcglobal.net", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard5 = new Klant { Voornaam = "Jan", Achternaam = "Vermassen", Gebruikersnaam = "Jan", Email = "jan@mac.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };

                Gebruiker gebruikerAdmin1TeamGdpr = new Administrator { Voornaam = "Team", Achternaam = "GDPR", Gebruikersnaam = "teamGDPR", Email = "info@teamgdpr.be", Wachtwoord = "Wachtwoord123", Rol = rolAdmin, Telefoonnummer = "0491234514" };
                Gebruiker gebruikerAdmin2QarfaRenate = new Administrator { Voornaam = "Renate", Achternaam = "Coen", Gebruikersnaam = "Renate", Email = "renate@qarfa.be", Wachtwoord = "Wachtwoord123", Rol = rolAdmin, Telefoonnummer = "0494157077" };

                Gebruiker gebruikerHandelaar1Qarfa = new Handelaar { Naam = "Qarfa", Voornaam = "Renate", Achternaam = "Coen", Gebruikersnaam = "Qarfa", Email = "info@qarfa.be", Wachtwoord = "Wachtwoord123", Rol = rolHandelaar, Telefoonnummer = "0494157077", Locatie =  new Locatie { Straat = "Stationsstraat", Huisnummer = "13", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.970252, Longitude = 3.984861 }, Website = "http://www.qarfa.be/", PromotieRange = 10, };
                Gebruiker gebruikerHandelaar2BrasserieBlomme = new Handelaar { Naam = "Brasserie Blomme", Voornaam = "Ann", Achternaam = "Blomme", Gebruikersnaam = "BrasserieBlomme", Email = "info@brasserieblomme.be", Wachtwoord = "Wachtwoord123", Rol = rolHandelaar, Telefoonnummer = "0475529592", Locatie =  new Locatie { Straat = "Gentsesteenweg", Huisnummer = "100", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.938074, Longitude = 4.024402 }, Website = "http://www.brasserieblomme.be/", PromotieRange = 2 };
                Gebruiker gebruikerHandelaar3Kelderman = new Handelaar { Naam = "Kelderman", Voornaam = "Dirk", Achternaam = "Kelderman", Gebruikersnaam = "Kelderman", Email = "info@kelderman.be", Wachtwoord = "Wachtwoord123", Rol = rolHandelaar, Telefoonnummer = "053776125", Locatie =  new Locatie { Straat = "Parklaan", Huisnummer = "4", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.892543, Longitude = 4.074539 }, Website = "http://www.visrestaurant-kelderman.be/", PromotieRange = 5 };
                Gebruiker gebruikerHandelaar4Zorba = new Handelaar { Naam = "Zorba Aalst", Voornaam = "Johan", Achternaam = "De Mulder", Gebruikersnaam = "ZorbaAalst", Email = "info@zorbaaalst.be", Wachtwoord = "Wachtwoord123", Rol = rolHandelaar, Telefoonnummer = "053776506", Locatie =  new Locatie { Straat = "Houtmarkt", Huisnummer = "3", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.934408, Longitude = 4.043971 }, Website = "https://www.facebook.com/pages/Zorba/140775739321413", PromotieRange = 0 };
                Gebruiker gebruikerHandelaar5Dion = new Handelaar { Naam = "Restaurant Dion", Voornaam = "John", Achternaam = "Dion", Gebruikersnaam = "Dion", Email = "info@Dion.be", Wachtwoord = "Wachtwoord123", Rol = rolHandelaar, Telefoonnummer = "053787815", Locatie =  new Locatie { Straat = "Oude Gentbaan", Huisnummer = "51", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.940219, Longitude = 4.017006 }, Website = "http://www.restaurantdion.be/", PromotieRange = 10 };



                var gebruikers = new List<Gebruiker>
                {
                    gebruikerStandaardLennert, gebruikerStandaard1, gebruikerStandaard2, gebruikerStandaard3, gebruikerStandaard4, gebruikerStandaard5,
                    gebruikerAdmin1TeamGdpr, gebruikerAdmin2QarfaRenate,
                    gebruikerHandelaar1Qarfa, gebruikerHandelaar2BrasserieBlomme, gebruikerHandelaar3Kelderman, gebruikerHandelaar4Zorba, gebruikerHandelaar5Dion
                };
                _dbContext.Gebruikers.AddRange(gebruikers);
                // GEBRUIKERS EIND

                //INGREDIENT
                IngredientInLunch ingredient1 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Paprika" } };
                IngredientInLunch ingredient2 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Kip" } };
                IngredientInLunch ingredient3 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Boontjes" } };
                IngredientInLunch ingredient4 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Melk producten" } };
                IngredientInLunch ingredient5 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Pasta" } };
                IngredientInLunch ingredient6 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Tomaat" } };
                IngredientInLunch ingredient7 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Brocoli" } };
                IngredientInLunch ingredient8 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Noten" } };
                IngredientInLunch ingredient9 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Rundsvlees" } };
                IngredientInLunch ingredient10 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Varkensvlees" } };
                IngredientInLunch ingredient11 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Vis" } };
                IngredientInLunch ingredient12 = new IngredientInLunch { Ingredient = new Ingredient { Naam = "Ui" } };

                var ingredienten1 = new List<IngredientInLunch>{
                    ingredient1, ingredient2, ingredient5, ingredient6, ingredient10
                };

                var ingredienten2 = new List<IngredientInLunch>{
                    ingredient1, ingredient6, ingredient3, ingredient6, ingredient9
                };

                var ingredienten3 = new List<IngredientInLunch>{
                    ingredient7, ingredient8, ingredient5, ingredient6, ingredient10
                };

                var ingredienten4 = new List<IngredientInLunch>{
                    ingredient1, ingredient2, ingredient5, ingredient6, ingredient10
                };

                var ingredienten5 = new List<IngredientInLunch>{
                    ingredient1, ingredient2, ingredient5, ingredient6, ingredient10
                };

                //Tags
                TagInLunch tag1 = new TagInLunch { Tag = new Tag { Naam = "hambuger" } };
                TagInLunch tag2 = new TagInLunch { Tag = new Tag { Naam = "lekker" } };
                TagInLunch tag3 = new TagInLunch { Tag = new Tag { Naam = "smakelijk" } };
                TagInLunch tag4 = new TagInLunch { Tag = new Tag { Naam = "pasta" } };
                TagInLunch tag5 = new TagInLunch { Tag = new Tag { Naam = "vis" } };

                var tags = new List<TagInLunch>{
                    tag1,tag2,tag3,tag4,tag5
                };

                //LUNCHES BEGIN
                Lunch lunchStandaard1 = new Lunch { Naam = "Hamburger", Prijs = 10, Ingredienten = ingredienten1, Beschrijving = "Deze smakelijke hamburger is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 11, 4), Tags = tags, Handelaar = (Handelaar)gebruikerHandelaar1Qarfa };
                Lunch lunchStandaard2 = new Lunch { Naam = "Pasta", Prijs = 34, Ingredienten = ingredienten2, Beschrijving = "Deze smakelijke pasta is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 11, 4), Tags = tags, Handelaar = (Handelaar)gebruikerHandelaar2BrasserieBlomme };
                Lunch lunchStandaard3 = new Lunch { Naam = "Vis Burger", Prijs = 15, Ingredienten = ingredienten3, Beschrijving = "Deze smakelijke vis burger is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 11, 4), Tags = tags, Handelaar = (Handelaar)gebruikerHandelaar3Kelderman };
                Lunch lunchStandaard4 = new Lunch { Naam = "Brocoli", Prijs = 25, Ingredienten = ingredienten4, Beschrijving = "Deze gezonde groente is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 11, 4), Tags = tags, Handelaar = (Handelaar)gebruikerHandelaar4Zorba };
                Lunch lunchStandaard5 = new Lunch { Naam = "Zalm", Prijs = 50, Ingredienten = ingredienten5, Beschrijving = "Deze smakelijke zalm is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 11, 4), Tags = tags, Handelaar = (Handelaar)gebruikerHandelaar5Dion };

                var lunches = new List<Lunch>{
                    lunchStandaard1,lunchStandaard2,lunchStandaard3,lunchStandaard4,lunchStandaard5
                };

                _dbContext.Lunches.AddRange(lunches);

                // SAVE CHANGES
                _dbContext.SaveChanges();
            }
        }
    }
}
