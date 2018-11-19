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
        public IEnumerable<Handelaar> Get()
        {
            return _handelaarRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Handelaar Get(int id)
        {
            return _handelaarRepository.GetById(id);
        }

    }
}
