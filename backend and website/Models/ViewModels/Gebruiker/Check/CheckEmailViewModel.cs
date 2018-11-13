using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.GebruikerViewModels
{
    public class CheckEmailViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress(ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Email { get; set; }
    }
}
