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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunchers.Controllers
{
    public class GebruikerController : Controller
    {
        private IConfiguration _config;
        IGebruikerRepository _gebruikerRepository;

        public GebruikerController(IConfiguration config, IGebruikerRepository gebruikerRepository)
        {
            _config = config;
            _gebruikerRepository = gebruikerRepository;
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
                    catch (System.Exception e)
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
                return Ok(new { emailBestaat = _gebruikerRepository.EmailExists(emailAanvraag.Email.ToLower()) });
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
                return Ok(new { gebruikersnaamBestaat = _gebruikerRepository.GebruikersnaamExists(gebruikersnaamAanvraag.Gebruikersnaam.ToLower()) });
            }
            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(new { error = foutboodschap });
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


                return Ok("temp");
            }

            //Als we hier zijn is is modelstate niet voldaan dus stuur error 400, slechte aanvraag
            string foutboodschap = ResultaatCustomNestdModelCheck(results);
            return BadRequest(new { error = "De ingevoerde waarden zijn onvolledig of voldoen niet aan de eisen voor een handelaar. Foutboodschap: " + foutboodschap });
        }

        private IActionResult RegistreerKlant(RegistreerGebruikerViewModel gebruikerAanvraag)
        {
            return Ok("temp");

        }

        private IActionResult RegistreerAdmin(RegistreerGebruikerViewModel gebruikerAanvraag)
        {
            return Ok("temp");

        }

        private static string ResultaatCustomNestdModelCheck(IEnumerable<ValidationResult> results)
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
    }
}
