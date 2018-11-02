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
    public class HandelaarController : Controller
    {
        IHandelaarRepository _handelaarRepository;

        public HandelaarController(IHandelaarRepository handelaarRepository)
        {
            _handelaarRepository = handelaarRepository;
        }

        [HttpGet]
        public Handelaar getById(int id)
        {
            return _handelaarRepository.getById(id);
        }

    }
}
