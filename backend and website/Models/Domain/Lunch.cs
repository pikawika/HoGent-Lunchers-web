using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class Lunch
    {
        [Key]
        public int LunchId { get; set; }
        public string Naam { get; set; }
        public double Prijs { get; set; }
        public List<IngredientInLunch> Ingredienten { get; set; } = new List<IngredientInLunch>();
        public string Beschrijving { get; set; }
        public List<Afbeelding> Afbeeldingen { get; set; } = new List<Afbeelding>();
        public DateTime BeginDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public List<TagInLunch> Tags { get; set; } = new List<TagInLunch>();
        public Handelaar Handelaar { get; set; }
    }
}
