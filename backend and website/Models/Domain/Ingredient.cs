using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        public string Naam { get; set; }
    }
}
