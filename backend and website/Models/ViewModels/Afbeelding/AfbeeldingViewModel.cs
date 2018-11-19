using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.ViewModels.Afbeelding
{
    public class AfbeeldingViewModel
    {
        [Required(ErrorMessage = "{0} is verplicht.")]
        [RegularExpression(@"^(http|https|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$")]
        public string Pad { get; set; }
    }
}
