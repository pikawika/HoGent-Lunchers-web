using Lunchers.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Klant : Gebruiker
    {
        public List<Favoriet> Favorieten { get; set; } = new List<Favoriet>();
        public List<Reservatie> Reservaties { get; set; } = new List<Reservatie>();
        public List<Allergy> Allergies { get; set; }
    }
}
