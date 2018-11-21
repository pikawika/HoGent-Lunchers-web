using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;
using System.Net.Http;
using System.Net;
using Lunchers.Models.GebruikerViewModels;
using Newtonsoft.Json.Linq;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Lunchers.Models.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Lunchers.Models.GebruikerViewModels.Login;
using Lunchers.Models.IRepositories;
using Lunchers.Models.GebruikerViewModels.GebruikerTaken;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class GebruikerController : Controller
    {
        private IConfiguration _config;
        private IGebruikerRepository _gebruikerRepository;
        private IRolRepository _rolRepository;

        public GebruikerController(IConfiguration config, IGebruikerRepository gebruikerRepository, IRolRepository rolRepository)
        {
            _config = config;
            _gebruikerRepository = gebruikerRepository;
            _rolRepository = rolRepository;
        }

        [HttpPost] 
        [AllowAnonymous]
        public IActionResult Registreer([FromBody]RegistreerGebruikerViewModel gebruikerAanvraag)
        {
            //gebruiker model moet bij eender welk type valid zijn
            if (ModelState.IsValid)
            {
                //handelaar
                if (gebruikerAanvraag.Login.Rol.ToLower() == "handelaar")
                {
                    try
                    {
                        //handelaarverzoek uit body halen
                        Stream req = Request.Body;
                        req.Seek(0, System.IO.SeekOrigin.Begin);
                        string json = new StreamReader(req).ReadToEnd();

                        //body converten naar handelaar registratie
                        RegistreerHandelaarViewModel handelaarAanvraag = JObject.Parse(json).ToObject<RegistreerHandelaarViewModel>();
                        return RegistreerHandelaar(handelaarAanvraag);
                    }
                    catch
                    {
                        return BadRequest(new { error = "Casting error, verkeerde datatype?" });
                    }

                }

                //klant
                if (gebruikerAanvraag.Login.Rol.ToLower() == "klant")
                {
                    return RegistreerKlant(gebruikerAanvraag);
                }

                //admin
                if (gebruikerAanvraag.Login.Rol.ToLower() == "admin")
                {
                    return RegistreerAdmin(gebruikerAanvraag);
                }
                return BadRequest(new { error = "Het soort gebruiker dat u wilt aanmaken bestaat niet (incorrecte rol)" });
            }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = "De ingevoerde waarden zijn onvolledig of voldoen niet aan de eisen voor een gebruiker. Foutboodschap: " + foutboodschap });


        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CheckEmailBestaat([FromBody]CheckEmailViewModel emailAanvraag)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { emailBestaat = CheckEmailBestaat(emailAanvraag.Email) });
            }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = foutboodschap });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CheckGebruikersnaamBestaat([FromBody]CheckGebruikerViewModel gebruikersnaamAanvraag)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { gebruikersnaamBestaat = CheckGebruikersnaamBestaat(gebruikersnaamAanvraag.Gebruikersnaam) });
            }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = foutboodschap });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginGebruikerViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = Login(login.Gebruikersnaam.ToLower(), login.Wachtwoord);

                //user gevonden dus aangemeld!
                if (user != null)
                {
                    var tokenString = BuildToken(user);
                    return Ok(new { token = tokenString });
                }

                //geen user gevonden
                if(CheckGebruikersnaamBestaat(login.Gebruikersnaam)){
                    return Unauthorized(new { error = "Incorrect wachtwoord en/of uw account is niet geactiveerd." });
                }else{
                    return Unauthorized(new { error = "Incorrecte gebruikersnaam." });
                }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = "De ingevoerde waarden zijn onvolledig of voldoen niet aan de eisen voor een login. Foutboodschap: " + foutboodschap });
        }

        [HttpPost]
        public IActionResult WijzigWachtwoord([FromBody]WijzigWachtwoordViewModel wijzigWachtwoordAanvraag)
        {
            if (ModelState.IsValid)
            {
                //token heeft geen id => fout met token!!
                if (User.FindFirst("gebruikersId")?.Value == null)
                    return BadRequest(new { error = "De voorziene token voldoet niet aan de eisen." });

                WijzigWachtwoord(int.Parse(User.FindFirst("gebruikersId")?.Value), wijzigWachtwoordAanvraag.Wachtwoord);

                return Ok(new { bericht = "Het wachtwoord is succesvol gewijzigd."  });
            }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = "De ingevoerde waarden zijn onvolledig of voldoen niet aan de eisen voor een login. Foutboodschap: " + foutboodschap });
        }

        private IActionResult RegistreerHandelaar(RegistreerHandelaarViewModel handelaarAanvraag)
        {
            //modelstate controleren van de NIEUWE gemaakte model
            var context = new ValidationContext(handelaarAanvraag, null, null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(handelaarAanvraag, context, results, true);

            if (isValid)
            {
                //long en latitude moeten voor validatie een string zijn, validatie moet er normaal voor zorgen dat ze altijd castable zijn naar double maar altijd goed om te checken.
                //checken of url geldig is
                Uri website;
                double latitude;
                double longitude;

                if (!double.TryParse(handelaarAanvraag.Locatie.Latitude, out latitude))
                    return BadRequest(new { error = "Latitude moet een string zijn die castable is naar double" });

                if (!double.TryParse(handelaarAanvraag.Locatie.Longitude, out longitude))
                    return BadRequest(new { error = "Longitude moet een string zijn die castable is naar double" });

                if (!Uri.TryCreate(handelaarAanvraag.Website, UriKind.Absolute, out website))
                    return BadRequest(new { error = "Ongeldige website" });

                if (CheckEmailBestaat(handelaarAanvraag.Email))
                    return BadRequest(new { error = "Het gekozen emailadres is reeds gekoppeld aan een account" });

                if (CheckGebruikersnaamBestaat(handelaarAanvraag.Login.Gebruikersnaam))
                    return BadRequest(new { error = "De gekozen gebruikersnaam is reeds gekoppeld aan een account" });

                Handelaar nieuweHandelaar = new Handelaar();

                nieuweHandelaar.Telefoonnummer = handelaarAanvraag.Telefoonnummer;
                nieuweHandelaar.Email = handelaarAanvraag.Email;
                nieuweHandelaar.Voornaam = handelaarAanvraag.Voornaam;
                nieuweHandelaar.Achternaam = handelaarAanvraag.Achternaam;

                nieuweHandelaar.Login.Gebruikersnaam = handelaarAanvraag.Login.Gebruikersnaam;
                nieuweHandelaar.Login.Geactiveerd = false;
                nieuweHandelaar.Login.Salt = MaakSalt();
                nieuweHandelaar.Login.Hash = MaakHash(handelaarAanvraag.Login.Wachtwoord, nieuweHandelaar.Login.Salt);

                nieuweHandelaar.Login.Rol = _rolRepository.GetByName(handelaarAanvraag.Login.Rol);

                nieuweHandelaar.HandelsNaam = handelaarAanvraag.HandelsNaam;
                if (handelaarAanvraag.Website != null)
                    nieuweHandelaar.Website = handelaarAanvraag.Website;
                nieuweHandelaar.PromotieRange = 0;

                nieuweHandelaar.Locatie.Straat = handelaarAanvraag.Locatie.Straat;
                nieuweHandelaar.Locatie.Huisnummer = handelaarAanvraag.Locatie.Huisnummer;
                nieuweHandelaar.Locatie.Postcode = handelaarAanvraag.Locatie.Postcode;
                nieuweHandelaar.Locatie.Gemeente = handelaarAanvraag.Locatie.Gemeente;
                nieuweHandelaar.Locatie.Latitude = latitude;
                nieuweHandelaar.Locatie.Longitude = longitude;

                nieuweHandelaar.Login.gebruiker = nieuweHandelaar;

                _gebruikerRepository.Registreer(nieuweHandelaar);

                return Ok(new { bericht = "Uw aanvraag om handelaar te worden is succesvol ingediend!" });
            }

            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = ResultaatCustomNestdModelCheck(results);
            return BadRequest(new { error = "De ingevoerde waarden zijn onvolledig of voldoen niet aan de eisen voor een handelaar. Foutboodschap: " + foutboodschap });
        }

        private IActionResult RegistreerKlant(RegistreerGebruikerViewModel klantAanvraag)
        {
            if (CheckEmailBestaat(klantAanvraag.Email))
                return BadRequest(new { error = "Het gekozen emailadres is reeds gekoppeld aan een account" });

            if (CheckGebruikersnaamBestaat(klantAanvraag.Login.Gebruikersnaam))
                return BadRequest(new { error = "De gekozen gebruikersnaam is reeds gekoppeld aan een account" });

            Klant nieuweKlant = new Klant();

            nieuweKlant.Telefoonnummer = klantAanvraag.Telefoonnummer;
            nieuweKlant.Email = klantAanvraag.Email;
            nieuweKlant.Voornaam = klantAanvraag.Voornaam;
            nieuweKlant.Achternaam = klantAanvraag.Achternaam;

            nieuweKlant.Login.Gebruikersnaam = klantAanvraag.Login.Gebruikersnaam;
            nieuweKlant.Login.Geactiveerd = true;
            nieuweKlant.Login.Salt = MaakSalt();
            nieuweKlant.Login.Hash = MaakHash(klantAanvraag.Login.Wachtwoord, nieuweKlant.Login.Salt);

            nieuweKlant.Login.Rol = _rolRepository.GetByName(klantAanvraag.Login.Rol);

            nieuweKlant.Login.gebruiker = nieuweKlant;

            _gebruikerRepository.Registreer(nieuweKlant);

            return Ok(new { token = BuildToken(nieuweKlant) });
        }

        private IActionResult RegistreerAdmin(RegistreerGebruikerViewModel adminAanvraag)
        {
            if (CheckEmailBestaat(adminAanvraag.Email))
                return BadRequest(new { error = "Het gekozen emailadres is reeds gekoppeld aan een account" });

            if (CheckGebruikersnaamBestaat(adminAanvraag.Login.Gebruikersnaam))
                return BadRequest(new { error = "De gekozen gebruikersnaam is reeds gekoppeld aan een account" });

            Administrator nieuweAdmin = new Administrator();

            nieuweAdmin.Telefoonnummer = adminAanvraag.Telefoonnummer;
            nieuweAdmin.Email = adminAanvraag.Email;
            nieuweAdmin.Voornaam = adminAanvraag.Voornaam;
            nieuweAdmin.Achternaam = adminAanvraag.Achternaam;

            nieuweAdmin.Login.Gebruikersnaam = adminAanvraag.Login.Gebruikersnaam;
            nieuweAdmin.Login.Geactiveerd = false;
            nieuweAdmin.Login.Salt = MaakSalt();
            nieuweAdmin.Login.Hash = MaakHash(adminAanvraag.Login.Wachtwoord, nieuweAdmin.Login.Salt);

            nieuweAdmin.Login.Rol = _rolRepository.GetByName(adminAanvraag.Login.Rol);

            nieuweAdmin.Login.gebruiker = nieuweAdmin;

            _gebruikerRepository.Registreer(nieuweAdmin);

            return Ok(new { bericht = "Uw aanvraag om handelaar te worden is succesvol ingediend!" });
        }

        private string ResultaatCustomNestdModelCheck(IEnumerable<ValidationResult> results)
        {
            string errors = "";
            foreach (var validationResult in results)
            {
                if (errors.Length != 0)
                    errors += " | ";

                errors += validationResult.ErrorMessage;

                //recursief de objecten in de objecten hun fouten ook tonen
                if (validationResult is CompositeValidationResult)
                {
                    if (errors.Length != 0)
                        errors += " | ";

                    errors += ResultaatCustomNestdModelCheck(((CompositeValidationResult)validationResult).Results);
                }
            }
            return errors;
        }

        private void WijzigWachtwoord(int gebruikersId, string nieuwWachtwoord)
        {
            // nieuwe salt voor beveiligingsreden
            byte[] nieuweSalt = MaakSalt();

            string nieuweHash = MaakHash(nieuwWachtwoord, nieuweSalt);

            _gebruikerRepository.WijzigWachtwoord(gebruikersId, nieuweSalt, nieuweHash);
        }

        private byte[] MaakSalt()
        {
            //maak een salt adhv een random nummer
            byte[] salt = new byte[128 / 8];
            using (var randomGetal = RandomNumberGenerator.Create())
            {
                randomGetal.GetBytes(salt);
            }

            return salt;
        }

        private string MaakHash(string wachtwoord, byte[] salt)
        {
            // build in hasher van .net (beste combo veilig en snel volgens microsoft documentatie)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: wachtwoord,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private bool CheckEmailBestaat(string email)
        {
            return _gebruikerRepository.EmailExists(email.ToLower());
        }

        private bool CheckGebruikersnaamBestaat(string gebruikersnaam)
        {
            return _gebruikerRepository.GebruikersnaamExists(gebruikersnaam.ToLower());
        }

        private bool CheckGebruikerIsGeactiveerd(string gebruikersnaam)
        {
            return _gebruikerRepository.GebruikerIsGeactiveerd(gebruikersnaam.ToLower());
        }

        private string BuildToken(Gebruiker gebruiker)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            token.Payload["gebruikersId"] = gebruiker.GebruikerId;
            token.Payload["gebruikersnaam"] = gebruiker.Login.Gebruikersnaam;
            token.Payload["rol"] = gebruiker.Login.Rol.Naam;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Gebruiker Login(string gebruikersnaam, string wachtwoord)
        {
            //indien geen geldige gebruikersnaam stop hier
            if (!CheckGebruikersnaamBestaat(gebruikersnaam))
                return null;

            //indien nog niet geactiveerd stop hier
            if (!CheckGebruikerIsGeactiveerd(gebruikersnaam))
                return null;

            byte[] salt = _gebruikerRepository.GetSalt(gebruikersnaam);

            if (salt == null)
            {
                throw new NotImplementedException();
            }

            string hash = MaakHash(wachtwoord, salt);

            //probeer login met de hash, indien foutief wachtwoord returnt dit ook null
            return _gebruikerRepository.Login(gebruikersnaam, hash);
        }

    }
}
