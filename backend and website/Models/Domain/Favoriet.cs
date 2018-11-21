using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Favoriet
    {
        [Key]
        public int FavorietId { get; set; }
        public DateTime DatumToegevoegd { get; set; }
        public Lunch Lunch { get; set; }
        public Klant Klant { get; set; }
    }
}
