using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;

namespace Lunchers.Controllers
{
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IGebruikerRepository _gebruikerRepository;

        public TokenController(IConfiguration config, IGebruikerRepository gebruikerRepository)
        {
            _config = config;
            this._gebruikerRepository = gebruikerRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string BuildToken(Gebruiker gebruiker)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Actort, gebruiker.Login.Rol.Naam),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Gebruiker Authenticate(LoginModel login)
        {
            return _gebruikerRepository.Authenticate(login.Gebruikersnaam, login.Wachtwoord);
        }




    }
}
