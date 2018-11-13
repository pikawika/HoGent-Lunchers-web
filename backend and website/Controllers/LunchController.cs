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
    [Route("api/[controller]")]
    public class LunchController : Controller
    {
        private ILunchRespository _lunchRespository;

        public LunchController(ILunchRespository lunchRespository)
        {
            _lunchRespository = lunchRespository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Lunch> Get()
        {
            return _lunchRespository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Lunch Get(int id)
        {
            return _lunchRespository.GetById(id);
        }

    }
}
