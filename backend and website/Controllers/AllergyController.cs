using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;
using Lunchers.Models.GebruikerViewModels.GebruikerTaken;
using Lunchers.Models.IRepositories;
using Lunchers.Models.Repositories;
using Lunchers.Models.ViewModels.Reservatie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AllergyController : ControllerBase
    {
        private IReservatieRepository _reservatieRepository;
        private IKlantRepository _klantRepository;
        private ILunchRespository _lunchRespository;
        private IHandelaarRepository _handelaarRepository;

        public AllergyController(IReservatieRepository reservatieRepository, IKlantRepository klantRepository, ILunchRespository lunchRespository, IHandelaarRepository handelaarRepository)
        {
            _reservatieRepository = reservatieRepository;
            _klantRepository = klantRepository;
            _lunchRespository = lunchRespository;
            _handelaarRepository = handelaarRepository;
        }

        // GET: api/Reservatie
        [HttpGet]
        public IActionResult Get()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                return Ok(klant.Allergies);
            }
            else if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                return Unauthorized(new { error = "U bent niet aangemeld als gebruiker." });
            }
            else if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                return Unauthorized(new { error = "U bent niet aangemeld als gebruiker." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld." });
        }

        // POST: api/Reservatie
        [HttpPost]
        public IActionResult Post([FromBody]AllergieToevoegenViewModel nieuweAllergie)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _klantRepository.AddAllergy(int.Parse(User.FindFirst("gebruikersId")?.Value),nieuweAllergie.Allergie);
                        return Ok(_klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value)).Allergies);
                    }
                    catch(Exception e)
                    {
                        return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de allergy." + e.Message });
                    }
                }
                return BadRequest(new { error = ModelState });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }
    }
}
