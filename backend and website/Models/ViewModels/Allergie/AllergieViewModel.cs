using System;
using System.ComponentModel.DataAnnotations;

namespace Lunchers.Models.ViewModels.Allergie
{
    public class AllergieViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(60)]
        public string Naam { get; set; }
    }
}
