using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Lunch
{
    public class LunchViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Naam { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public double Prijs { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public string Beschrijving { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime BeginDatum { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime EindDatum { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int HandelaarId { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<int> Ingredienten { get; set; }
        [Required(ErrorMessage = "{0} is verplicht.")]
        public List<int> Tags { get; set; }
    }
}
