using Lunchers.Models.ViewModels.Lunch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Reservatie
{
    public class ReservatieViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int LunchId { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Range(1, 50)]
        public int Aantal { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
    }
}
