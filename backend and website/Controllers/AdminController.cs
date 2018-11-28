using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
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

        public AdminController(IHandelaarRepository handelaarRepository)
        {
            _handelaarRepository = handelaarRepository;
        }

        [HttpPost]
        public IActionResult KeurHandelaarGoed([FromBody]HandelaarKeuringViewModel handelaarGoedkeuring)
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
        public IActionResult KeurHandelaarAf([FromBody]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    handelaar.Login.Geactiveerd = false;
                    _handelaarRepository.SaveChanges();

                    return Ok(new { bericht = "De handelaar werd succesvol afgekeurd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpDelete]
        public IActionResult VerwijderHandelaar([FromBody]HandelaarKeuringViewModel handelaarGoedkeuring)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                Handelaar handelaar = _handelaarRepository.GetById(handelaarGoedkeuring.HandelaarId);
                if (handelaar != null)
                {
                    _handelaarRepository.Delete(handelaar);
                    _handelaarRepository.SaveChanges();

                    return Ok(new { bericht = "De handelaar werd succesvol verwijderd." });
                }
                return BadRequest(new { error = "De opgegeven handelaar werd niet teruggevonden." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }
    }
}