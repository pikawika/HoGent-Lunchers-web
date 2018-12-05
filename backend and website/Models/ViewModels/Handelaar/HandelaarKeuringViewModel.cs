using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Handelaar
{
    public class HandelaarKeuringViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        public int HandelaarId { get; set; }
    }
}
