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
                // ROLLEN EINDE

                // GEBRUIKERS BEGIN
                Gebruiker gebruikerStandaardLennert = new Klant { Voornaam = "Lennert", Achternaam = "Bontinck", Gebruikersnaam = "lennert", Email = "info@lennertbontinck.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234514", Rol = rolStandaard };
                Gebruiker gebruikerStandaard1 = new Klant { Voornaam = "Kathi", Achternaam = "Bramblett", Gebruikersnaam = "kathi", Email = "bramblett@me.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard2 = new Klant { Voornaam = "Liza", Achternaam = "Imboden", Gebruikersnaam = "liza", Email = "liza@optonline.net", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard3 = new Klant { Voornaam = "Christine", Achternaam = "Heisler", Gebruikersnaam = "christine", Email = "christine@msn.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard4 = new Klant { Voornaam = "Jena", Achternaam = "Ocampo", Gebruikersnaam = "jena", Email = "jena@sbcglobal.net", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };
                Gebruiker gebruikerStandaard5 = new Klant { Voornaam = "Jan", Achternaam = "Vermassen", Gebruikersnaam = "jan", Email = "jan@mac.com", Wachtwoord = "Wachtwoord123", Telefoonnummer = "0491234515", Rol = rolStandaard };

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

                // GEBRUIKERS EINDE

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

                var ingredientenVlees = new List<IngredientInLunch>{
                    ingredient9, ingredient10, ingredient12, ingredient6, ingredient9, ingredient10, ingredient12
                };

                var ingredientenVegan = new List<IngredientInLunch>{
                    ingredient1, ingredient3, ingredient6, ingredient4, ingredient6, ingredient7, ingredient8
                };

                var ingredientenPasta = new List<IngredientInLunch>{
                    ingredient1, ingredient2, ingredient5, ingredient6, ingredient8
                };

                var ingredientenVis = new List<IngredientInLunch>{
                    ingredient11, ingredient7
                };
                //INGREDIENT EINDE

                //TAGS
                TagInLunch tag1 = new TagInLunch { Tag = new Tag { Naam = "hambuger" } };
                TagInLunch tag2 = new TagInLunch { Tag = new Tag { Naam = "vlees" } };
                TagInLunch tag3 = new TagInLunch { Tag = new Tag { Naam = "frietjes" } };
                TagInLunch tag4 = new TagInLunch { Tag = new Tag { Naam = "sla" } };
                TagInLunch tag5 = new TagInLunch { Tag = new Tag { Naam = "gezond" } };
                TagInLunch tag6 = new TagInLunch { Tag = new Tag { Naam = "vegetarisch" } };
                TagInLunch tag7 = new TagInLunch { Tag = new Tag { Naam = "vis" } };
                TagInLunch tag8 = new TagInLunch { Tag = new Tag { Naam = "zalm" } };
                TagInLunch tag9 = new TagInLunch { Tag = new Tag { Naam = "italiaans" } };
                TagInLunch tag10 = new TagInLunch { Tag = new Tag { Naam = "dieet" } };
                TagInLunch tag11 = new TagInLunch { Tag = new Tag { Naam = "kip" } };


                var tagsVlees = new List<TagInLunch>{
                    tag1,tag2,tag3
                };

                var tagsVegan = new List<TagInLunch>{
                    tag4, tag5, tag6, tag10
                };

                var tagsPasta = new List<TagInLunch>{
                    tag4, tag10, tag9, tag11
                };

                var tagsVis = new List<TagInLunch>{
                    tag7, tag8, tag10
                };
                //TAGS EINDE


                //LUNCHES BEGIN
                //nog Afbeeldingen
                Lunch lunchStandaardHamburger = new Lunch { Naam = "Hamburger met frietjes", Prijs = 10, Ingredienten = ingredientenVlees, Beschrijving = "Deze smakelijke hamburger is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVlees, Handelaar = (Handelaar)gebruikerHandelaar1Qarfa };
                Lunch lunchStandaardPasta = new Lunch { Naam = "Italiaanse pasta", Prijs = 34, Ingredienten = ingredientenPasta, Beschrijving = "Deze smakelijke pasta is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsPasta, Handelaar = (Handelaar)gebruikerHandelaar2BrasserieBlomme };
                Lunch lunchStandaardVis = new Lunch { Naam = "Visschotel", Prijs = 15, Ingredienten = ingredientenVis, Beschrijving = "Deze smakelijke vis burger is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVis, Handelaar = (Handelaar)gebruikerHandelaar3Kelderman };
                Lunch lunchStandaardVegan = new Lunch { Naam = "Vegan salad", Prijs = 25, Ingredienten = ingredientenVegan, Beschrijving = "Deze gezonde groente is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVegan, Handelaar = (Handelaar)gebruikerHandelaar4Zorba };
                Lunch lunchStandaardZalm = new Lunch { Naam = "Zalm met venkel", Prijs = 50, Ingredienten = ingredientenVis, Beschrijving = "Deze smakelijke zalm is nu te verkrijgen!", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Tags = tagsVis, Handelaar = (Handelaar)gebruikerHandelaar5Dion };

                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(lunchStandaardHamburger);
                ((Handelaar)gebruikerHandelaar2BrasserieBlomme).Lunches.Add(lunchStandaardPasta);
                ((Handelaar)gebruikerHandelaar3Kelderman).Lunches.Add(lunchStandaardVis);
                ((Handelaar)gebruikerHandelaar4Zorba).Lunches.Add(lunchStandaardVegan);
                ((Handelaar)gebruikerHandelaar5Dion).Lunches.Add(lunchStandaardZalm);
                //LUNCHES EINDE

                //AFBEELDINGEN
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/1.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/2.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/3.jpg" });

                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/1.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/2.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/3.jpg" });

                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/1.jpg" });
                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/2.jpg" });

                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/1.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/2.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/3.jpg" });

                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/1.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/2.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/3.jpg" });
                //AFBEELDINGEN EINDE

                //RESERVATIES -> user lennert
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 5, Datum = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 3, Datum = new DateTime(2018, 10, 25), Lunch = lunchStandaardPasta });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 2, Datum = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 1, Datum = new DateTime(2018, 10, 31), Lunch = lunchStandaardVegan });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 9, Datum = new DateTime(2018, 10, 30), Lunch = lunchStandaardZalm });
                //RESERVATIES EINDE

                //FAVORIETEN -> user lennert
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 30), Lunch = lunchStandaardVegan });
                //FAVORIETEN EINDE

                // SAVE CHANGES
                _dbContext.Rollen.AddRange(rollen);
                _dbContext.Gebruikers.AddRange(gebruikers);
                _dbContext.SaveChanges();
            }
        }
    }
}