using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lunchers.Models;
using Lunchers.Models.Repositories;
using Lunchers.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Lunchers.Models.ViewModels.Lunch;
using System.IO;
using Newtonsoft.Json.Linq;

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

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]LunchViewModel nieuweLunch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Stream req = Request.Body;
                    req.Seek(0, System.IO.SeekOrigin.Begin);
                    string json = new StreamReader(req).ReadToEnd();

                    LunchViewModel lunch = JObject.Parse(json).ToObject<LunchViewModel>();
                    return Ok(new { bericht = "De lunch werd succesvol aangemaakt." });
                }
                catch
                {
                    return BadRequest(new { error = "Er is iets fout gegaan tijdens het aanmaken van de lunch." });
                }
            }
            return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Lunch lunch =_lunchRespository.GetAll().SingleOrDefault(l => l.LunchId == id);
            _lunchRespository.Delete(lunch);
            _lunchRespository.SaveChanges();
        }

    }
}
