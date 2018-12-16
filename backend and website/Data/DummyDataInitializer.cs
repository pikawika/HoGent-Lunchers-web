using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Lunchers.Controllers;
using Lunchers.Models;
using Lunchers.Models.Domain;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
                Rol rolAdmin = new Rol { Naam = "admin" };
                Rol rolStandaard = new Rol { Naam = "klant" };
                Rol rolHandelaar = new Rol { Naam = "handelaar" };

                var rollen = new List<Rol>
                {
                    rolAdmin, rolStandaard, rolHandelaar
                };
                // ROLLEN EINDE

                // GEBRUIKERS BEGIN
                Gebruiker gebruikerStandaardLennert = new Klant { Voornaam = "Lennert", Achternaam = "Bontinck", Email = "info@lennertbontinck.com", Telefoonnummer = "+32491234514" };
                gebruikerStandaardLennert.Login = new Login { Gebruikersnaam = "lennert", Rol = rolStandaard, Geactiveerd = true };
                Gebruiker gebruikerStandaard1 = new Klant { Voornaam = "Kathi", Achternaam = "Bramblett", Email = "bramblett@me.com", Telefoonnummer = "+32491234515" };
                gebruikerStandaard1.Login = new Login { Gebruikersnaam = "kathi", Rol = rolStandaard, Geactiveerd = true };
                Gebruiker gebruikerStandaard2 = new Klant { Voornaam = "Liza", Achternaam = "Imboden", Email = "liza@optonline.net", Telefoonnummer = "+32491234515" };
                gebruikerStandaard2.Login = new Login { Gebruikersnaam = "liza", Rol = rolStandaard, Geactiveerd = true };
                Gebruiker gebruikerStandaard3 = new Klant { Voornaam = "Christine", Achternaam = "Heisler", Email = "christine@msn.com", Telefoonnummer = "+32491234515" };
                gebruikerStandaard3.Login = new Login { Gebruikersnaam = "christine", Rol = rolStandaard, Geactiveerd = true };
                Gebruiker gebruikerStandaard4 = new Klant { Voornaam = "Jena", Achternaam = "Ocampo", Email = "jena@sbcglobal.net", Telefoonnummer = "+32491234515" };
                gebruikerStandaard4.Login = new Login { Gebruikersnaam = "jena", Rol = rolStandaard, Geactiveerd = true };
                Gebruiker gebruikerStandaard5 = new Klant { Voornaam = "Jan", Achternaam = "Vermassen", Email = "jan@mac.com", Telefoonnummer = "+32491234515" };
                gebruikerStandaard5.Login = new Login { Gebruikersnaam = "jan", Rol = rolStandaard, Geactiveerd = true };

                Gebruiker gebruikerAdmin1TeamGdpr = new Administrator { Voornaam = "Team", Achternaam = "GDPR", Email = "lunchersteam@gmail.com", Telefoonnummer = "+32491234514" };
                gebruikerAdmin1TeamGdpr.Login = new Login { Gebruikersnaam = "teamGDPR", Rol = rolAdmin, Geactiveerd = true };
                Gebruiker gebruikerAdmin2QarfaRenate = new Administrator { Voornaam = "Renate", Achternaam = "Coen", Email = "admin@lunchers.com", Telefoonnummer = "+32494157077" };
                gebruikerAdmin2QarfaRenate.Login = new Login { Gebruikersnaam = "Renate", Rol = rolAdmin, Geactiveerd = true };

                Gebruiker gebruikerHandelaar1Qarfa = new Handelaar { HandelsNaam = "Qarfa", Voornaam = "Renate", Achternaam = "Coen", Email = "example1@teamgdpr.com", Telefoonnummer = "+32494157077", Locatie = new Locatie { Straat = "Stationsstraat", Huisnummer = "13", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.941878, Longitude = 4.0372882 }, Website = "http://www.qarfa.be/", PromotieRange = 10 };
                gebruikerHandelaar1Qarfa.Login = new Login { Gebruikersnaam = "qarfa", Rol = rolHandelaar, Geactiveerd = true };
                Gebruiker gebruikerHandelaar2BrasserieBlomme = new Handelaar { HandelsNaam = "Brasserie Blomme", Voornaam = "Ann", Achternaam = "Blomme", Email = "example2@teamgdpr.com", Telefoonnummer = "+32475529592", Locatie = new Locatie { Straat = "Gentsesteenweg", Huisnummer = "100", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.938074, Longitude = 4.024402 }, Website = "http://www.brasserieblomme.be/", PromotieRange = 2 };
                gebruikerHandelaar2BrasserieBlomme.Login = new Login { Gebruikersnaam = "blomme", Rol = rolHandelaar, Geactiveerd = true };
                Gebruiker gebruikerHandelaar3Kelderman = new Handelaar { HandelsNaam = "Kelderman", Voornaam = "Dirk", Achternaam = "Kelderman", Email = "example3@teamgdpr.come", Telefoonnummer = "+3253776125", Locatie = new Locatie { Straat = "Parklaan", Huisnummer = "4", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.892543, Longitude = 4.074539 }, Website = "http://www.visrestaurant-kelderman.be/", PromotieRange = 5 };
                gebruikerHandelaar3Kelderman.Login = new Login { Gebruikersnaam = "kelderman", Rol = rolHandelaar, Geactiveerd = true };
                Gebruiker gebruikerHandelaar4Zorba = new Handelaar { HandelsNaam = "Zorba Aalst", Voornaam = "Johan", Achternaam = "De Mulder", Email = "example4@teamgdpr.com", Telefoonnummer = "+3253776506", Locatie = new Locatie { Straat = "Houtmarkt", Huisnummer = "3", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.934408, Longitude = 4.043971 }, Website = "https://www.facebook.com/pages/Zorba/140775739321413", PromotieRange = 0 };
                gebruikerHandelaar4Zorba.Login = new Login { Gebruikersnaam = "zorba", Rol = rolHandelaar, Geactiveerd = true };
                Gebruiker gebruikerHandelaar5Dion = new Handelaar { HandelsNaam = "Restaurant Dion", Voornaam = "John", Achternaam = "Dion", Email = "example5@teamgdpr.com", Telefoonnummer = "+3253787815", Locatie = new Locatie { Straat = "Oude Gentbaan", Huisnummer = "51", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.940219, Longitude = 4.017006 }, Website = "http://www.restaurantdion.be/", PromotieRange = 10 };
                gebruikerHandelaar5Dion.Login = new Login { Gebruikersnaam = "dion", Rol = rolHandelaar, Geactiveerd = true };

                Gebruiker testgebruiker = new Handelaar { HandelsNaam = "testgebruiker", Voornaam = "John", Achternaam = "Dion", Email = "info@test.be", Telefoonnummer = "+3254787815", Locatie = new Locatie { Straat = "Oude Gentbaan", Huisnummer = "51", Postcode = "9300 ", Gemeente = "Aalst", Latitude = 50.940219, Longitude = 4.017006 }, Website = "http://www.restaurantdion.be/", PromotieRange = 10 };
                testgebruiker.Login = new Login { Gebruikersnaam = "testgebruiker", Rol = rolHandelaar, Geactiveerd = true };



                var gebruikers = new List<Gebruiker>
                {
                    gebruikerStandaardLennert, gebruikerStandaard1, gebruikerStandaard2, gebruikerStandaard3, gebruikerStandaard4, gebruikerStandaard5,
                    gebruikerAdmin1TeamGdpr, gebruikerAdmin2QarfaRenate,
                    gebruikerHandelaar1Qarfa, gebruikerHandelaar2BrasserieBlomme, gebruikerHandelaar3Kelderman, gebruikerHandelaar4Zorba, gebruikerHandelaar5Dion,testgebruiker
                };



                // GEBRUIKERS EINDE

                //INGREDIENT
                Ingredient ingredientPaprika = new Ingredient { Naam = "Paprika" };
                Ingredient ingredientKip = new Ingredient { Naam = "Kip" };
                Ingredient ingredientBoontjes = new Ingredient { Naam = "Boontjes" };
                Ingredient ingredientMelk = new Ingredient { Naam = "Melk producten" };
                Ingredient ingredientPasta = new Ingredient { Naam = "Pasta" };
                Ingredient ingredientTomaat = new Ingredient { Naam = "Tomaat" };
                Ingredient ingredientBrocoli = new Ingredient { Naam = "Brocoli" };
                Ingredient ingredientKaas = new Ingredient { Naam = "Kaas" };
                Ingredient ingredientNoten = new Ingredient { Naam = "Noten" };
                Ingredient ingredientRundsvlees = new Ingredient { Naam = "Rundsvlees" };
                Ingredient ingredientVarkensvlees = new Ingredient { Naam = "Varkensvlees" };
                Ingredient ingredientVis = new Ingredient { Naam = "Vis" };
                Ingredient ingredientUi = new Ingredient { Naam = "Ui" };
                Ingredient ingredientSla = new Ingredient { Naam = "Bergsla" };
                Ingredient ingredientFriet = new Ingredient { Naam = "Frietjes" };
                //INGREDIENT EINDE -> nog in lijst van lunch steken

                //TAGS
                string RodeKleur = "FF6A6A";
                string GroeneKleur = "82CA9D ";
                string Gelekleur = "FFF79A ";

                /*
                    vegan
                    vlees
                    vis
                    pasta
                    fastfood
                    gluttenvrij
                    lactosevrij
                 */

                Tag tagVegan = new Tag { Naam = "Vegan", Kleur = RodeKleur };
                Tag tagVlees = new Tag { Naam = "Vlees", Kleur = RodeKleur };
                Tag tagPasta = new Tag { Naam = "Pasta", Kleur = GroeneKleur };
                Tag tagGlutten = new Tag { Naam = "Gluten", Kleur = GroeneKleur };
                Tag tagSalade = new Tag { Naam = "Sla", Kleur = GroeneKleur };
                Tag tagLactose = new Tag { Naam = "Lactose", Kleur = GroeneKleur };
                Tag tagVis = new Tag { Naam = "Vis", Kleur = RodeKleur };
                Tag tagFastfood = new Tag { Naam = "Fastfood", Kleur = RodeKleur };
                Tag tagRoomsaus = new Tag { Naam = "Room", Kleur = Gelekleur };
                //TAGS EINDE -> nog in lijst van lunch steken


                //LUNCHES BEGIN
                //nog Afbeeldingen
                Lunch lunchStandaardHamburger = new Lunch { Naam = "American hamburger", Prijs = 10.00, Beschrijving = "Een echte American burger met alles wat er bij hoort zoals bacon, cheddar kaas en augurkjes.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };
                Lunch lunchStandaardPasta = new Lunch { Naam = "Italiaanse pasta rosbief", Prijs = 20, Beschrijving = "Rosbief is een klassieker, maar waarom niet eens combineren met pasta en lekkere Italiaanse producten?", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };
                Lunch lunchStandaardVis = new Lunch { Naam = "Visschotel", Prijs = 15.50, Beschrijving = "Gegratineerde visschotel met duo van puree op grootmoeders wijze.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };
                Lunch lunchStandaardVegan = new Lunch { Naam = "Vegan salad", Prijs = 25.00, Beschrijving = "Een lekker frisse en bovenal gezonde vegan salade.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };
                Lunch lunchStandaardZalm = new Lunch { Naam = "Zalm met venkel", Prijs = 50.00, Beschrijving = "Zalm vergezeld met venkel en heerlijke roomsaus op oma's wijze", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };
                Lunch lunchStandaardBiefstuk = new Lunch { Naam = "Biefstuk met frietjes", Prijs = 22.50, Beschrijving = "Wat smaakt er beter dan een lekkere steak, zeker wanneer die nét goed gebakken is? Bleu, saignant, à point of bien cuit: u zegt het, wij bakken het.", BeginDatum = new DateTime(2018, 10, 30), EindDatum = new DateTime(2018, 12, 30), Deleted = false };

                Lunch LunchUitzonderingVervallen = new Lunch { Naam = "Schotse Hamburger", Prijs = 12.50, Beschrijving = "Een hamburger met een kilt", BeginDatum = new DateTime(2017, 10, 30), EindDatum = new DateTime(2017, 12, 30), Deleted = true };

                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(lunchStandaardHamburger);
                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(LunchUitzonderingVervallen);
                ((Handelaar)gebruikerHandelaar1Qarfa).Lunches.Add(lunchStandaardBiefstuk);
                ((Handelaar)gebruikerHandelaar2BrasserieBlomme).Lunches.Add(lunchStandaardPasta);
                ((Handelaar)gebruikerHandelaar3Kelderman).Lunches.Add(lunchStandaardVis);
                ((Handelaar)gebruikerHandelaar4Zorba).Lunches.Add(lunchStandaardVegan);
                ((Handelaar)gebruikerHandelaar5Dion).Lunches.Add(lunchStandaardZalm);
                //LUNCHES EINDE

                //AFBEELDINGEN
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/1.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/2.jpg" });
                lunchStandaardHamburger.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch1/3.jpg" });
                lunchStandaardHamburger.LunchTags.Add(new LunchTag { Tag = tagFastfood });
                lunchStandaardHamburger.LunchTags.Add(new LunchTag { Tag = tagVlees });
                lunchStandaardHamburger.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientVarkensvlees });
                lunchStandaardHamburger.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientPaprika });
                lunchStandaardHamburger.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientKaas });
                lunchStandaardHamburger.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientUi });

                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/1.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/2.jpg" });
                lunchStandaardPasta.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch2/3.jpg" });
                lunchStandaardPasta.LunchTags.Add(new LunchTag { Tag = tagLactose });
                lunchStandaardPasta.LunchTags.Add(new LunchTag { Tag = tagGlutten });
                lunchStandaardPasta.LunchTags.Add(new LunchTag { Tag = tagVlees });
                lunchStandaardPasta.LunchTags.Add(new LunchTag { Tag = tagPasta });
                lunchStandaardPasta.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientPasta });
                lunchStandaardPasta.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientKip });

                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/1.jpg" });
                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/2.jpg" });
                lunchStandaardVis.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch3/3.jpg" });
                lunchStandaardVis.LunchTags.Add(new LunchTag { Tag = tagVis });
                lunchStandaardVis.LunchTags.Add(new LunchTag { Tag = tagLactose });
                lunchStandaardVis.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientSla });
                lunchStandaardVis.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientVis });
                lunchStandaardVis.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientPaprika });

                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/1.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/2.jpg" });
                lunchStandaardVegan.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch4/3.jpg" });
                lunchStandaardVegan.LunchTags.Add(new LunchTag { Tag = tagVegan });
                lunchStandaardVegan.LunchTags.Add(new LunchTag { Tag = tagSalade });
                lunchStandaardVegan.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientSla });
                lunchStandaardVegan.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientBrocoli });
                lunchStandaardVegan.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientTomaat });

                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/1.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/2.jpg" });
                lunchStandaardZalm.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch5/3.jpg" });
                lunchStandaardZalm.LunchTags.Add(new LunchTag { Tag = tagVis });
                lunchStandaardZalm.LunchTags.Add(new LunchTag { Tag = tagRoomsaus });
                lunchStandaardZalm.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientVis });
                lunchStandaardZalm.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientMelk });

                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/1.jpg" });
                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/2.jpg" });
                lunchStandaardBiefstuk.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch6/3.jpg" });
                lunchStandaardBiefstuk.LunchTags.Add(new LunchTag { Tag = tagVlees });
                lunchStandaardBiefstuk.LunchTags.Add(new LunchTag { Tag = tagRoomsaus });
                lunchStandaardBiefstuk.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientRundsvlees });
                lunchStandaardBiefstuk.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientFriet });
                lunchStandaardBiefstuk.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientMelk });

                LunchUitzonderingVervallen.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch7/1.jpg" });
                LunchUitzonderingVervallen.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch7/2.jpg" });
                LunchUitzonderingVervallen.Afbeeldingen.Add(new Afbeelding { Pad = "lunches/lunch7/3.jpg" });
                LunchUitzonderingVervallen.LunchTags.Add(new LunchTag { Tag = tagFastfood });
                LunchUitzonderingVervallen.LunchTags.Add(new LunchTag { Tag = tagVlees });
                LunchUitzonderingVervallen.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientVarkensvlees });
                LunchUitzonderingVervallen.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientKaas });
                LunchUitzonderingVervallen.LunchIngredienten.Add(new LunchIngredient { Ingredient = ingredientPaprika });
                //AFBEELDINGEN EINDE

                //RESERVATIES -> user lennert
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 1, Datum = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger, Status = Status.Goedgekeurd, Opmerking="kan ik extra bacon krijgen :-)" });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 3, Datum = new DateTime(2018, 10, 25), Lunch = lunchStandaardPasta, Status = Status.Afgekeurd, Opmerking = ""  });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 2, Datum = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis, Status = Status.InAfwachting, Opmerking = "" });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 1, Datum = new DateTime(2018, 10, 31), Lunch = lunchStandaardVegan, Status = Status.InAfwachting, Opmerking = "" });
                ((Klant)gebruikerStandaardLennert).Reservaties.Add(new Reservatie { Aantal = 9, Datum = new DateTime(2018, 10, 30), Lunch = lunchStandaardZalm, Status = Status.Goedgekeurd, Opmerking = "" });

                // --> user niet lennert
                ((Klant)gebruikerStandaard1).Reservaties.Add(new Reservatie { Aantal = 1, Datum = new DateTime(2018, 10, 31), Lunch = lunchStandaardVegan, Status = Status.Goedgekeurd, Opmerking = "" });
                ((Klant)gebruikerStandaard2).Reservaties.Add(new Reservatie { Aantal = 9, Datum = new DateTime(2018, 10, 30), Lunch = lunchStandaardZalm, Status = Status.InAfwachting, Opmerking= "" });
                //RESERVATIES EINDE

                //FAVORIETEN -> user lennert
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 26), Lunch = lunchStandaardHamburger });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 26), Lunch = LunchUitzonderingVervallen });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaardLennert).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 30), Lunch = lunchStandaardVegan });

                // -> user niet lennert
                ((Klant)gebruikerStandaard1).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 28), Lunch = lunchStandaardVis });
                ((Klant)gebruikerStandaard2).Favorieten.Add(new Favoriet { DatumToegevoegd = new DateTime(2018, 10, 30), Lunch = lunchStandaardVegan });
                //FAVORIETEN EINDE
                //FAVORIETEN EINDE

                //WACHTWOORDEN TOEKENNEN
                byte[] salt = new byte[128 / 8];
                using (var randomGetal = RandomNumberGenerator.Create())
                {
                    randomGetal.GetBytes(salt);
                }

                string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: "Wachtwoord123",
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                foreach (Gebruiker gebruiker in gebruikers)
                {
                    gebruiker.Login.Salt = salt;
                    gebruiker.Login.Hash = hash;
                }
                //WACHTWOORDEN TOEKENNEN EINDE

                // SAVE CHANGES
                _dbContext.Rollen.AddRange(rollen);
                _dbContext.Gebruikers.AddRange(gebruikers);
                _dbContext.SaveChanges();
            }
        }
    }
}