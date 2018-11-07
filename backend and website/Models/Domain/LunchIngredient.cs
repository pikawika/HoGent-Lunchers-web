using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class LunchIngredient
    {
        public int LunchId { get; set; }
        public Lunch Lunch { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
