using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.GebruikerViewModels.GebruikerTaken
{
    public class WijzigWachtwoordViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Password, ErrorMessage = "Geen geldig {0} ingevoerd")]
        [StringLength(30, ErrorMessage = "Het wachtwoord moet tussen {2} en {1} karakters lang zijn.", MinimumLength = 6)]
        public string Wachtwoord { get; set; }
    }
}
