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
        public String ShowRoll()
        {
            
            var currentUser = HttpContext.User;


            if (currentUser.HasClaim(c => c.Type == ClaimTypes.Actor))
            {
                string rol = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor).Value;

                if (rol == "Admin")
                {
                    return "Je bent een Admin";
                }else{
                    return "Je bent geen Admin maar wel " + rol;
                }
            }


            return null;
        }
    }
}
