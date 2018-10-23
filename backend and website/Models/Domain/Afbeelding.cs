using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Afbeelding
    {
        [Key]
        public int AfbeeldingId { get; private set; }
        public string Pad { get; set; }
    }
}
