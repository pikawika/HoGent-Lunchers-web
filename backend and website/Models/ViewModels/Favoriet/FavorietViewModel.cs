using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Favoriet
{
    public class FavorietViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int LunchId { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
    }
}
