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
using System.Diagnostics;
using Newtonsoft.Json;

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
        private IKlantRepository _klantRepository;
        private ITagRepository _tagRepository;

        public LunchController(ILunchRespository lunchRespository,
                               IHandelaarRepository handelaarRepository,
                               IAfbeeldingRepository afbeeldingRepository,
                               IIngredientRepository ingredientRepository,
                               ITagRepository tagRepository,
                               IKlantRepository klantRepository)
        {
            _lunchRespository = lunchRespository;
            _handelaarRepository = handelaarRepository;
            _afbeeldingRepository = afbeeldingRepository;
            _ingredientRepository = ingredientRepository;
            _klantRepository = klantRepository;
            _tagRepository = tagRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Lunch> Get([FromQuery]double latitude, [FromQuery]double longitude)
        {
            // Als de locatie meegegeven wordt, wordt gezocht op locatie
            if (latitude != 0 && longitude != 0)
            {
                if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
                {
                    Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                    if (klant.Allergies.Count > 0)
                    {
                        List<Lunch> lunches = new List<Lunch>();
                        foreach (Lunch lunch in _lunchRespository.GetAllFromLocation(latitude, longitude))
                        {
                            if (!ContainsAllergy(klant.Allergies, lunch.LunchIngredienten, lunch.LunchTags))
                            {
                                lunches.Add(lunch);
                            }

                        }
                        return lunches.AsEnumerable();
                    }
                    else
                    {
                        return _lunchRespository.GetAllFromLocation(latitude, longitude);
                    }
                }
            }
            // Zonder locatie worden alle geldige lunches meegegeven in omgekeerde volgorde(van nieuw naar oud)
            else
                if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "klant")
            {
                Klant klant = _klantRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));
                if (klant.Allergies.Count > 0)
                {
                    List<Lunch> lunches = new List<Lunch>();
                    foreach (Lunch lunch in _lunchRespository.GetAll())
                    {
                        if (!ContainsAllergy(klant.Allergies, lunch.LunchIngredienten, lunch.LunchTags))
                        {
                            lunches.Add(lunch);
                        }

                    }
                    return lunches.AsEnumerable().Reverse();
                }
            }
            return _lunchRespository.GetAll().Reverse();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (User.FindFirst("gebruikersId")?.Value != null && User.FindFirst("rol")?.Value == "handelaar")
            {
                try
                {
                    Handelaar handelaar = _handelaarRepository.GetByIdInternal(int.Parse(User.FindFirst("gebruikersId")?.Value));

                    Lunch lunch = _lunchRespository.GetById(id);

                    if (handelaar == lunch.Handelaar)
                    {
                        _handelaarRepository.RemoveLunch(handelaar.GebruikerId, lunch.LunchId);
                        return Ok(new { bericht = "De lunch werd succesvol verwijderd." });
                    }
                    return BadRequest(new { error = "De lunch behoort niet toe aan de aangemelde handelaar." });
                }
                catch (Exception e)
                {
                    return BadRequest(new { error = "Er is een onverwachte fout opgetreden tijdens het aanpassen van de lunch. " + e.Message.ToString().ToLower() });
                }
            }
            return Unauthorized(new { error = "U bent niet aangemeld als handelaar." });

        }


        private bool ContainsAllergy(List<Allergy> allergies, List<LunchIngredient> ingredients, List<LunchTag> tags)
        {
            bool has_allergy = false;
            foreach (Allergy allergy in allergies)
            {
                foreach (LunchIngredient lunch_ingredient in ingredients)
                {
                    if (allergy.AllergyNaam.Equals(lunch_ingredient.Ingredient.Naam, StringComparison.InvariantCultureIgnoreCase))
                    {
                        has_allergy = true;
                        break;
                    }
                }

                //nutteloos om nog verder te filteren indien er al een allergie gevonden is
                if (!has_allergy)
                {
                    foreach (LunchTag lunch_tag in tags)
                    {
                        if (allergy.AllergyNaam.Equals(lunch_tag.Tag.Naam, StringComparison.InvariantCultureIgnoreCase))
                        {
                            has_allergy = true;
                            break;
                        }
                    }
                }
            }
            return has_allergy;
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


                        Debug.WriteLine(nieuweLunch.ToString());
                        Handelaar handelaar = _handelaarRepository.GetById(int.Parse(User.FindFirst("gebruikersId")?.Value));

                        string stringIngredients = nieuweLunch.RawData["Ingredienten"];
                        string stringTags = nieuweLunch.RawData["Tags"];

                        List<IngredientViewModel> ingredienten = JsonConvert.DeserializeObject<List<IngredientViewModel>>(stringIngredients);
                        List<TagViewModel> tags = JsonConvert.DeserializeObject<List<TagViewModel>>(stringTags);

                        Lunch lunch = new Lunch()
                        {
                            Naam = nieuweLunch.Naam,
                            Prijs = double.Parse(nieuweLunch.Prijs),
                            Beschrijving = nieuweLunch.Beschrijving,
                            BeginDatum = nieuweLunch.BeginDatum,
                            EindDatum = nieuweLunch.EindDatum,
                            LunchIngredienten = ConvertIngredientViewModelsToIngredienten(ingredienten),
                            LunchTags = ConvertTagViewModelsToTags(tags),
                            Deleted = false,
                        };

                        handelaar.Lunches.Add(lunch);
                        _handelaarRepository.SaveChanges();

                        if (nieuweLunch.Afbeeldingen.Files.Count != 0)
                        {
                            lunch.Afbeeldingen = await ConvertFormFilesToAfbeeldingenAsync(nieuweLunch.Afbeeldingen.Files.ToList(), lunch);
                        }
                        else
                        {
                            List<Afbeelding> afbeeldingen = new List<Afbeelding>();
                            string afbeeldingRelativePath = "lunches/tagfoto/" + lunch.LunchTags[0].Tag.Naam + ".jpg";
                            afbeeldingen.Add(new Afbeelding { Pad = afbeeldingRelativePath });
                            string filePath = @"wwwroot" + afbeeldingRelativePath;
                            lunch.Afbeeldingen = afbeeldingen;
                        }
                        _lunchRespository.SaveChanges();
                        return Ok(new { bericht = "De lunch werd succesvol aangemaakt." });

                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { error = "Er is een onverwachte fout opgetreden tijdens het aanmaken van de nieuwe lunch. " + e.Message.ToString().ToLower() });
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
                        Handelaar handelaar = _handelaarRepository.GetByIdInternal(int.Parse(User.FindFirst("gebruikersId")?.Value));

                        Lunch lunch = _lunchRespository.GetById(id);

                        if (handelaar == lunch.Handelaar)
                        {
                            if (aangepasteLunch.BeginDatum.Date >= DateTime.Now.Date && aangepasteLunch.EindDatum.Date >= DateTime.Now.Date && aangepasteLunch.BeginDatum.Date <= aangepasteLunch.EindDatum.Date)
                            {
                                string stringIngredients = aangepasteLunch.RawData["Ingredienten"];
                                string stringTags = aangepasteLunch.RawData["Tags"];

                                List<IngredientViewModel> ingredienten = JsonConvert.DeserializeObject<List<IngredientViewModel>>(stringIngredients);
                                List<TagViewModel> tags = JsonConvert.DeserializeObject<List<TagViewModel>>(stringTags);

                                lunch.Naam = aangepasteLunch.Naam;
                                lunch.Prijs = double.Parse(aangepasteLunch.Prijs);
                                lunch.Beschrijving = aangepasteLunch.Beschrijving;
                                lunch.BeginDatum = aangepasteLunch.BeginDatum;
                                lunch.EindDatum = aangepasteLunch.EindDatum;
                                lunch.LunchIngredienten = ConvertIngredientViewModelsToIngredienten(ingredienten);
                                lunch.LunchTags = ConvertTagViewModelsToTags(tags);

                                if (aangepasteLunch.Afbeeldingen.Files.Count != 0) lunch.Afbeeldingen = await ConvertFormFilesToAfbeeldingenAsync(aangepasteLunch.Afbeeldingen.Files.ToList(), lunch);

                                _lunchRespository.SaveChanges();

                                return Ok(new { bericht = "De lunch werd succesvol bijgewerkt." });
                            }
                            else
                            {
                                return BadRequest(new { error = "Er is iets mis met de begin- en/of einddatum." });
                            }

                        }

                        return BadRequest(new { error = "De lunch behoort niet toe aan de aangemelde handelaar." });

                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { error = "Er is een onverwachte fout opgetreden tijdens het aanpassen van de lunch. " + e.Message.ToString().ToLower() });
                    }
                }
                return BadRequest(new { error = "De opgestuurde gegevens zijn onvolledig of incorrect." });
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
                await afbeeldingFiles[(i - 1)].CopyToAsync(fileStream);
                fileStream.Close();
            }

            return afbeeldingen;
        }

        private List<LunchIngredient> ConvertIngredientViewModelsToIngredienten(List<IngredientViewModel> ingredientvms)
        {

            List<LunchIngredient> ingredienten = new List<LunchIngredient>();
            Debug.WriteLine(ingredientvms.Count);
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
                    if (tvm.Kleur == null) tag = new Tag { Naam = tvm.Naam, Kleur = "#000000" };
                    else tag = new Tag { Naam = tvm.Naam, Kleur = tvm.Kleur };
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
