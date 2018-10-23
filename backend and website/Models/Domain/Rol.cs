using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lunchers.Models.Domain
{
    public class Rol
    {
        [Key]
        public int RolId { get; set; }
        public string Naam { get; set; }
    }
}
