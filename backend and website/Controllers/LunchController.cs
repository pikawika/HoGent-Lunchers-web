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
using Lunchers.Models.ViewModels.Afbeelding;
using Lunchers.Models.ViewModels.Ingredient;
using Lunchers.Models.ViewModels.Tag;
using Lunchers.Models.IRepositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lunchers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LunchController : Controller
    {
        private ILunchRespository _lunchRespository;
        private IHandelaarRepository _handelaarRepository;
        private IAfbeeldingRepository _afbeeldingRepository;
        private IIngredientRepository _ingredientRepository;
        private ITagRepository _tagRepository;

        public LunchController(ILunchRespository lunchRespository, IHandelaarRepository handelaarRepository, IAfbeeldingRepository afbeeldingRepository, IIngredientRepository ingredientRepository, ITagRepository tagRepository)
        {
            _lunchRespository = lunchRespository;
            _handelaarRepository = handelaarRepository;
            _afbeeldingRepository = afbeeldingRepository;
            _ingredientRepository = ingredientRepository;
            _tagRepository = tagRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Lunch> Get()
        {
            return _lunchRespository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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
                    LunchViewModel lunchvm = JObject.Parse(json).ToObject<LunchViewModel>();

                    Handelaar handelaar = _handelaarRepository.GetAll().SingleOrDefault(h => h.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value));

                    Lunch lunch = new Lunch()
                    {
                        Naam = lunchvm.Naam,
                        Prijs = lunchvm.Prijs,
                        Beschrijving = lunchvm.Beschrijving,
                        BeginDatum = lunchvm.BeginDatum,
                        EindDatum = lunchvm.EindDatum,
                        Afbeeldingen = ConvertAfbeeldingViewModelsToAfbeeldingen(lunchvm.Afbeeldingen),
                        LunchIngredienten = ConvertIngredientViewModelsToIngredienten(lunchvm.Ingredienten),
                        LunchTags = ConvertTagViewModelsToTags(lunchvm.Tags),
                    };

                    handelaar.Lunches.Add(lunch);
                    _handelaarRepository.SaveChanges();

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

        #region Helper Functies
        private List<Afbeelding> ConvertAfbeeldingViewModelsToAfbeeldingen(List<AfbeeldingViewModel> afbeeldingvms)
        {
            List<Afbeelding> afbeeldingen = new List<Afbeelding>();
            foreach (AfbeeldingViewModel avm in afbeeldingvms)
            {
                Afbeelding afbeelding = _afbeeldingRepository.GetAll().SingleOrDefault(a => a.Pad == avm.Pad);
                if (afbeelding == null)
                {
                    afbeelding = new Afbeelding { Pad = avm.Pad };
                    _afbeeldingRepository.Add(afbeelding);
                    _afbeeldingRepository.SaveChanges();
                }
                afbeeldingen.Add(afbeelding);
            }
            return afbeeldingen;
        }

        private List<LunchIngredient> ConvertIngredientViewModelsToIngredienten(List<IngredientViewModel> ingredientvms)
        {
            List<LunchIngredient> ingredienten = new List<LunchIngredient>();
            foreach (IngredientViewModel ivm in ingredientvms)
            {
                Ingredient ingredient = _ingredientRepository.GetAll().SingleOrDefault(i => i.Naam == ivm.Naam);
                if (ingredient == null)
                {
                    ingredient = new Ingredient { Naam = ivm.Naam };
                    _ingredientRepository.Add(ingredient);
                    _ingredientRepository.SaveChanges();
                }
                LunchIngredient lunchIngredient = new LunchIngredient { Ingredient = ingredient };
                ingredienten.Add(lunchIngredient);
            }
            return ingredienten;
        }

        private List<LunchTag> ConvertTagViewModelsToTags(List<TagViewModel> tagvms)
        {
            List<LunchTag> tags = new List<LunchTag>();
            foreach (TagViewModel tvm in tagvms)
            {
                Tag tag = _tagRepository.GetAll().SingleOrDefault(t => t.Naam == tvm.Naam);
                if (tag == null)
                {
                    tag = new Tag { Naam = tvm.Naam, Kleur = tvm.Kleur };
                    _tagRepository.Add(tag);
                    _tagRepository.SaveChanges();
                }
                LunchTag lunchTag = new LunchTag { Tag = tag };
                tags.Add(lunchTag);
            }
            return tags;
        }
        #endregion
    }
}
