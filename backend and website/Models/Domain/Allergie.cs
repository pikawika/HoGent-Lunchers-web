using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class Allergie
    {
        [Key]
        public int AllergieId { get; set; }
        public string Naam { get; set; }
        [JsonIgnore]
        public List<KlantAllergie> KlantAllergies { get; set; }
    }
}
