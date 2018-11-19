using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Afbeelding
{
    public class AfbeeldingViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Pad { get; set; }
    }
}
