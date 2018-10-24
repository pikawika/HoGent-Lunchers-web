using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Locatie
    {
        [Key]
        public int LocatieId { get; private set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
