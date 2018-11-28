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
using Lunchers.Models.ViewModels.Ingredient;
using Lunchers.Models.ViewModels.Tag;
using Lunchers.Models.IRepositories;
using Microsoft.AspNetCore.Http;

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
            return _lunchRespository.GetAll().Reverse();
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
        public async Task<IActionResult> Post([FromForm]LunchViewModel nieuweLunch)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (nieuweLunch.Afbeeldingen.Files.Count != 0)
                        {
                            Handelaar handelaar = _handelaarRepository.GetAll().SingleOrDefault(h => h.GebruikerId == int.Parse(User.FindFirst("gebruikersId")?.Value));

                            Lunch lunch = new Lunch()
                            {
                                Naam = nieuweLunch.Naam,
                                Prijs = double.Parse(nieuweLunch.Prijs),
                                Beschrijving = nieuweLunch.Beschrijving,
                                BeginDatum = nieuweLunch.BeginDatum,
                                EindDatum = nieuweLunch.EindDatum,
                                LunchIngredienten = ConvertIngredientViewModelsToIngredienten(nieuweLunch.Ingredienten),
                                LunchTags = ConvertTagViewModelsToTags(nieuweLunch.Tags),
                            };

                            handelaar.Lunches.Add(lunch);
                            _handelaarRepository.SaveChanges();

                            lunch.Afbeeldingen = await ConvertFormFilesToAfbeeldingenAsync(nieuweLunch.Afbeeldingen.Files.ToList(), lunch);
                            _lunchRespository.SaveChanges();

                            return Ok(new { bericht = "De lunch werd succesvol aangemaakt." });
                        }
                        return BadRequest(new { error = "Gelieve minstens één afbeelding meesturen." });
                    }
                    catch
                    {
                        return BadRequest(new { error = "Er is een onverwachte fout opgetreden tijdens het aanmaken van de nieuwe lunch." });
                    }
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als handelaar." });
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm]LunchEditViewModel aangepasteLunch)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Handelaar handelaar = _handelaarRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));

                        Lunch lunch = _lunchRespository.GetById(id);

                        if (handelaar == lunch.Handelaar) {
                            lunch.Naam = aangepasteLunch.Naam;
                            lunch.Prijs = aangepasteLunch.Prijs;
                            lunch.Beschrijving = aangepasteLunch.Beschrijving;
                            lunch.BeginDatum = aangepasteLunch.BeginDatum;
                            lunch.EindDatum = aangepasteLunch.EindDatum;
                            lunch.LunchIngredienten = ConvertIngredientViewModelsToIngredienten(aangepasteLunch.Ingredienten);
                            lunch.LunchTags = ConvertTagViewModelsToTags(aangepasteLunch.Tags);

                            if (aangepasteLunch.Afbeeldingen != null)
                            {
                                string path = @"wwwroot" + "/lunches/lunch" + lunch.LunchId;
                                Directory.Delete(path);

                                lunch.Afbeeldingen = await ConvertFormFilesToAfbeeldingenAsync(aangepasteLunch.Afbeeldingen, lunch);
                            }

                            _lunchRespository.SaveChanges();

                            return Ok(new { bericht = "De lunch werd succesvol bijgewerkt." });
                        }

                        return BadRequest(new { error = "De lunch behoort niet toe aan de aangemelde handelaar." });

                    }
                    catch (Exception e)
                    {
                        //return BadRequest(new { error = "Er is iets fout gegaan tijdens het bijwerken van de lunch." });
                        return BadRequest(new { error = e });
                    }
                }
                //return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
                return BadRequest(new { error = ModelState });
            }
            return Unauthorized(new { error = "U bent niet aangemeld als handelaar." });
        }

        #region Helper Functies
        private async Task<List<Afbeelding>> ConvertFormFilesToAfbeeldingenAsync(List<IFormFile> afbeeldingFiles, Lunch lunch)
        {
            List<Afbeelding> afbeeldingen = new List<Afbeelding>();

            for (int i = 1; i <= afbeeldingFiles.Count; i++)
            {
                string afbeeldingRelativePath = "/lunches/lunch" + lunch.LunchId + "/" + i + ".jpg";
                afbeeldingen.Add(new Afbeelding { Pad = afbeeldingRelativePath });
                string filePath = @"wwwroot" + afbeeldingRelativePath;
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                FileStream fileStream = new FileStream(filePath, FileMode.Create);
                await afbeeldingFiles[(i-1)].CopyToAsync(fileStream);
                fileStream.Close();
            }

            return afbeeldingen;
        }

        private List<LunchIngredient> ConvertIngredientViewModelsToIngredienten(List<IngredientViewModel> ingredientvms)
        {
            List<LunchIngredient> ingredienten = new List<LunchIngredient>();
            foreach (IngredientViewModel ivm in ingredientvms)
            {
                Ingredient ingredient = _ingredientRepository.GetByName(ivm.Naam);
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
                Tag tag = _tagRepository.GetByName(tvm.Naam);
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
