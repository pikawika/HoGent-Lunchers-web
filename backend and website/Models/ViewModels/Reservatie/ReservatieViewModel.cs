using Lunchers.Models.ViewModels.Lunch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Reservatie
{
    public class ReservatieViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int LunchId { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Range(1, 50)]
        public int Aantal { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        
        public String Opmerking { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (Datum.Date < DateTime.Now.Date)
            {
                results.Add(new ValidationResult("Datum moet vandaag of later zijn.", new[] { "BeginDatum" }));
            }

            return results;
        }
    }
}
