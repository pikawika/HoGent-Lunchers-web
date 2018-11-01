using Lunchers.Models.Domain;
using Lunchers.Models.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.GebruikerViewModels
{
    public class RegistreerHandelaarViewModel : RegistreerGebruikerViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string HandelsNaam { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Url, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Website { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [ValidateObject]
        public RegistreerLocatieViewModel Locatie { get; set; }


    }
}