using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class IngredientInLunch
    {
        [Key]
        public int IngredientInLunchId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
