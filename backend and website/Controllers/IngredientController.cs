using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lunchers.Data.Repositories;
using Lunchers.Models.Domain;
using Lunchers.Models.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lunchers.Controllers
{
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/Ingredient
        [HttpGet]
        public IEnumerable<Ingredient> Get()
        {
            return _ingredientRepository.GetAll().ToList();
        }

        // GET: api/Ingredient/5
        [HttpGet("{id}", Name = "Get")]
        public Ingredient Get(int id)
        {
            return _ingredientRepository.GetById(id);
        }
    }
}
