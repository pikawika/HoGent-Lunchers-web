using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;
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
    public class ReservatieController : ControllerBase
    {
        private IReservatieRepository _reservatieRepository;
        private IKlantRepository _klantRepository;
        private ILunchRespository _lunchRespository;

        public ReservatieController(IReservatieRepository reservatieRepository, IKlantRepository klantRepository, ILunchRespository lunchRespository)
        {
            _reservatieRepository = reservatieRepository;
            _klantRepository = klantRepository;
            _lunchRespository = lunchRespository;
        }

        // GET: api/Reservatie
        [HttpGet]
        public IActionResult Get()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                List<Reservatie> reservaties = _reservatieRepository.GetAllFromCustomer(int.Parse(User.FindFirst("gebruikersId")?.Value)).ToList();
                return Ok(new { reservaties });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // GET: api/Reservatie/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                Reservatie reservatie = _reservatieRepository.GetById(id);
                if (reservatie != null)
                {
                    if (reservatie.Klant.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value))
                    {
                        return Ok(new { reservatie });
                    }
                    return Unauthorized(new { error = "Deze reservatie behoort niet toe aan deze klant." });
                }
                return BadRequest(new { error = "Deze reservatie bestaat niet." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // POST: api/Reservatie
        [HttpPost]
        public IActionResult Post([FromBody]ReservatieViewModel nieuweReservatie)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                        Lunch lunch = _lunchRespository.GetById(nieuweReservatie.LunchId);

                        if (klant != null && lunch != null)
                        {
                            if (nieuweReservatie.Datum >= lunch.BeginDatum && nieuweReservatie.Datum <= lunch.EindDatum)
                            {
                                Reservatie reservatie = new Reservatie
                                {
                                    Lunch = lunch,
                                    Aantal = nieuweReservatie.Aantal,
                                    Datum = nieuweReservatie.Datum
                                };

                                klant.Reservaties.Add(reservatie);
                                _reservatieRepository.SaveChanges();

                                return Ok(new { bericht = "De reservatie werd succesvol aangemaakt." });
                            }
                            return BadRequest(new { error = "De gekozen lunch is niet beschikbaar op de opgegeven datum." });
                        }
                        return BadRequest(new { error = "De opgegeven lunch of klant kon niet worden teruggevonden." });
                    }
                    catch
                    {
                        return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de reservatie." });
                    }
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }
    }
}
