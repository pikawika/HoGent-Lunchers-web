using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Models;
using Lunchers.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lunchers.Controllers
{
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // GET: api/Tag
        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _tagRepository.GetAll().ToList();
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public Tag Get(int id)
        {
            return _tagRepository.GetById(id);
        }
    }
}
