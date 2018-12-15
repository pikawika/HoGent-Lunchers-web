using Lunchers.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.GebruikerViewModels
{
    public class RegistreerGebruikerViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Telefoonnummer { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [EmailAddress(ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Logingegevens zijn verplicht.")]
        public RegistreerLoginViewModel Login { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Text, ErrorMessage = "Geen geldig {0} ingevoerd")]
        public string Achternaam { get; set; }
    }
}