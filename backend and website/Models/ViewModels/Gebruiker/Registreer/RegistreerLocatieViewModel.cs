using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.GebruikerViewModels
{
    public class RegistreerLocatieViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Straat { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Huisnummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Gemeente { get; set; }
    }
}
