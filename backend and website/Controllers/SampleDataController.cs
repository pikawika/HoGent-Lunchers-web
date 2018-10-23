using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

namespace Lunchers.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private IGebruikerRepository _users;

        public SampleDataController(IGebruikerRepository users)
        {
            _users = users;
        }

       [HttpGet("[action]"), Authorize]
        public IEnumerable<Gebruiker> WeatherForecasts()
        {
            
            var currentUser = HttpContext.User;


            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Actor))
            {
                string rol = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value;

                if (rol == "admin")
                {
                    string[] test = new string[] { "warm", "koud", "lol" };
                    //returned momenteel steeds een lijst van alle users 
                    //om te testen of de user wel degelijk toegevoegd is
                    return _users.GetAll();
                }else{
                    string[] test = new string[] { "Rol moet admin zijn maar is " +rol };
                    //returned momenteel steeds een lijst van alle users 
                    //om te testen of de user wel degelijk toegevoegd is
                    return _users.GetAll();
                }
            }


            return null;
        }
    }
}
