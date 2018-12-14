using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class Reservatie
    {
        [Key]
        public int ReservatieId { get; set; }
        public Lunch Lunch { get; set; }
        public int Aantal { get; set; }
        public DateTime Datum { get; set; }
        public Klant Klant { get; set; }
        public Status Status { get; set; }
        public String Opmerking { get; set; }
    }
}
