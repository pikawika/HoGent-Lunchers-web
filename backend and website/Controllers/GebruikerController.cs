using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunchers.Controllers
{
    [Route("api/[controller]")]
    public class GebruikerController : Controller
    {
        IGebruikerRepository _gebruikerRepository;

        public GebruikerController(IGebruikerRepository gebruikerRepository)
        {
            _gebruikerRepository = gebruikerRepository;
        }

        [HttpPost]
        public IEnumerable<string> Registreer([FromBody]Gebruiker gebruiker)
        {
            _gebruikerRepository.Registreer(gebruiker);
            return new string[] { "Gebruikersnaam", gebruiker.Gebruikersnaam };
        }

    }
}
