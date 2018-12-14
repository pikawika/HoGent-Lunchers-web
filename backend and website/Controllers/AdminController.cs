using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.IRepositories;
using Lunchers.Models.Repositories;
using Lunchers.Models.ViewModels.Handelaar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AdminController : Controller
    {
        private IHandelaarRepository _handelaarRepository;
        private IReservatieRepository _reservatieRepository;
        private ILunchRespository _lunchRespository;

        public AdminController(IHandelaarRepository handelaarRepository, IReservatieRepository reservatieRepository, ILunchRespository lunchRespository)
        {
            _handelaarRepository = handelaarRepository;
            _reservatieRepository = reservatieRepository;
            _lunchRespository = lunchRespository;
        }

        [HttpPost]
        public IActionResult KeurHandelaarGoed([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    handelaar.Login.Geactiveerd = true;
                    _handelaarRepository.SaveChanges();

                    return Ok(new { bericht = "De handelaar werd succesvol goedgekeurd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpPost]
        public IActionResult KeurHandelaarAf([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    handelaar.Login.Geactiveerd = false;
                    _handelaarRepository.Delete(handelaar.GebruikerId);
                    _handelaarRepository.SaveChanges();

                    return Ok(new { bericht = "De handelaar werd succesvol afgekeurd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpPost]
        public IActionResult VerwijderHandelaar([FromForm]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    _handelaarRepository.Delete(handelaar.GebruikerId);
                    _handelaarRepository.SaveChanges();

                    return Ok(new { bericht = "De handelaar werd succesvol verwijderd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpGet]
        public IActionResult KrijgAantallen()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                var aantallen = new
                {
                    AantalHandelaars = _handelaarRepository.GetAll().Count(),
                    AantalLunches = _lunchRespository.GetAll().Count(),
                    AantalReservaties = _reservatieRepository.GetAll().Count()
                };

                return Ok(aantallen);
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }
    }
}