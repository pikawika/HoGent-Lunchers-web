using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Ingredient
{
    public class IngredientViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [StringLength(60)]
        public string Naam { get; set; }
    }
}
