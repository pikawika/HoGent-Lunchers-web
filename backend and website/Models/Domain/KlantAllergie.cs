using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class KlantAllergie
    {
        public int KlantId { get; set; }
        public Klant Klant { get; set; }
        public int AllergieId { get; set; }
        public Allergie Allergie { get; set; }
    }
}
