using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
        public bool Geactiveerd { get; set; }
        public Rol Rol { get; set; } = new Rol();

        public Gebruiker gebruiker { get; set; }
        public int gebruikerLoginId { get; set; }
    }
}
