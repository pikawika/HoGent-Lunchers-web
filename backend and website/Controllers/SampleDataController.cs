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

        /*private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]"), Authorize]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }*/

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
                    //return test.AsEnumerable();
                    return _users.GetAll();
                }else{
                    string[] test = new string[] { "Rol moet admin zijn maar is " +rol };
                    //return test.AsEnumerable();
                    return _users.GetAll();
                }
            }


            return null;
        }
    }
}
