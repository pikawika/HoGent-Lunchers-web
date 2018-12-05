using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Reservatie
{
    public class ReservatieEditViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int ReservatieId { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public Status Status { get; set; }
    }
}
