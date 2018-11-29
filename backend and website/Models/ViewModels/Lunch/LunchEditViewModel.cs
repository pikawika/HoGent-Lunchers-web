using Lunchers.Models.Domain;
using Lunchers.Models.ViewModels.Ingredient;
using Lunchers.Models.ViewModels.Tag;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Lunch
{
    public class LunchEditViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(60, MinimumLength = 3)]
        public string Naam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Range(1, 999.99)]
        public double Prijs { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(2500, MinimumLength = 10)]
        public string Beschrijving { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime BeginDatum { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime EindDatum { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<IngredientViewModel> Ingredienten { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<TagViewModel> Tags { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile> Afbeeldingen { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (BeginDatum.Date < DateTime.Now.Date)
            {
                results.Add(new ValidationResult("Begindatum moet vandaag of later zijn.", new[] { "BeginDatum" }));
            }

            if (EindDatum < BeginDatum)
            {
                results.Add(new ValidationResult("Einddatum mag niet eerder zijn dan begindatum.", new[] { "EindDatum" }));
            }

            return results;
        }
    }
}
