using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class Handelaar : Gebruiker
    {
        public string HandelsNaam { get; set; }
        public Locatie Locatie { get; set; }
        public string Website { get; set; }
        public List<Lunch> Lunches { get; set; } = new List<Lunch>();
        public int PromotieRange { get; set; }
    }
}
