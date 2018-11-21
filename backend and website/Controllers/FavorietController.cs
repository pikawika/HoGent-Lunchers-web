using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Lunchers.Models.Repositories;
using Lunchers.Models.ViewModels.Favoriet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FavorietController : ControllerBase
    {
        private IFavorietRepository _favorietRepository;
        private IKlantRepository _klantRepository;
        private ILunchRespository _lunchRespository;

        public FavorietController(IFavorietRepository favorietRepository, IKlantRepository klantRepository, ILunchRespository lunchRespository)
        {
            _favorietRepository = favorietRepository;
            _klantRepository = klantRepository;
            _lunchRespository = lunchRespository;
        }

        // GET: api/Favoriet
        [HttpGet]
        public IActionResult Get()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                List<Favoriet> favorieten = _favorietRepository.GetAllFromCustomer(int.Parse(User.FindFirst("gebruikersId")?.Value)).ToList();
                return Ok(new { favorieten });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // GET: api/Favoriet/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                Favoriet favoriet = _favorietRepository.GetById(id);
                if (favoriet != null)
                {
                    if (favoriet.Klant.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value))
                    {
                        return Ok(new { favoriet });
                    }
                    return Unauthorized(new { error = "Deze favoriet behoort niet toe aan deze klant." });
                }
                return BadRequest(new { error = "Deze favoriet bestaat niet." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // POST: api/Favoriet
        [HttpPost]
        public IActionResult Post([FromBody]FavorietViewModel nieuweFavoriet)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                        Lunch lunch = _lunchRespository.GetById(nieuweFavoriet.LunchId);

                        if (klant != null && lunch != null)
                        {
                            Favoriet favoriet = new Favoriet
                            {
                                Lunch = lunch,
                                DatumToegevoegd = nieuweFavoriet.Datum
                            };

                            klant.Favorieten.Add(favoriet);
                            _favorietRepository.SaveChanges();

                            return Ok(new { bericht = "De favoriet werd succesvol aangemaakt." });
                        }
                        return BadRequest(new { error = "De opgegeven lunch of klant kon niet worden teruggevonden." });
                    }
                    catch
                    {
                        return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de favoriet." });
                    }
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                        Favoriet favoriet = _favorietRepository.GetById(id);

                        if (favoriet.Klant.GebruikerId == klant.GebruikerId)
                        {
                            _favorietRepository.Delete(favoriet);
                            _favorietRepository.SaveChanges();

                            return Ok(new { bericht = "De favoriet werd succesvol verwijderd." });
                        }
                        return BadRequest(new { error = "De opgegeven favoriet behoort niet tot deze gebruiker." });
                    }
                    catch
                    {
                        return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de favoriet." });
                    }
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als klant." });
        }
    }
}
