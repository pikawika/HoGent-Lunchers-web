using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Gebruiker
    {
        [Key]
        public int GebruikerId { get; private set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Email { get; set; }
        public string Wachtwoord { get; set; }
        public string Telefoonnummer { get; set; }
        public Rol Rol { get; set; }
        public List<Favoriet> Favorieten { get; set; }
        public List<Reservatie> Reservaties { get; set; }
    }
}
