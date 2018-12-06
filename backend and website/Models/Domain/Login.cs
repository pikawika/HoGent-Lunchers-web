using Newtonsoft.Json;
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
        [JsonIgnore]
        public int LoginId { get; set; }
        [JsonIgnore]
        public string Gebruikersnaam { get; set; }
        [JsonIgnore]
        public string Hash { get; set; }
        [JsonIgnore]
        public byte[] Salt { get; set; }

        public bool Geactiveerd { get; set; }

        [JsonIgnore]
        public Rol Rol { get; set; } = new Rol();

        [JsonIgnore]
        public Gebruiker gebruiker { get; set; }
        [JsonIgnore]
        public int gebruikerLoginId { get; set; }
    }
}
