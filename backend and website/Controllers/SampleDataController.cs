using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using WebApplication5.Models.Repositories;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private IUserRepository _users;

        public SampleDataController(IUserRepository users)
        {
            _users = users;
        }

       [HttpGet("[action]"), Authorize]
        public IEnumerable<UserModel> WeatherForecasts()
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
