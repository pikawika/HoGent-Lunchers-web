using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class HandelaarController : Controller
    {
        private IHandelaarRepository _handelaarRepository;

        public HandelaarController(IHandelaarRepository handelaarRepository)
        {
            _handelaarRepository = handelaarRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "admin")
            {
                List<Handelaar> handelaars = _handelaarRepository.GetAll().ToList();
                return Ok(new { handelaars });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als administrator." });
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && (User.FindFirst("rol")?.Value == "handelaar" || User.FindFirst("rol")?.Value == "admin"))
            {
                Handelaar handelaar = _handelaarRepository.GetById(id);
                if (handelaar != null)
                {
                    if (handelaar.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value) || User.FindFirst("rol")?.Value == "admin")
                    {
                        return Ok(new { handelaar });
                    }
                    return BadRequest(new { error = "U bent niet aangemeld als de opgevraagde handelaar en bent ook geen administrator." });
                }
                return BadRequest(new { error = "De opgevraagde handelaar bestaat niet" });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als handelaar of administrator." });
        }

    }
}
