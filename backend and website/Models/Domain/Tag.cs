using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; private set; }
        public string Naam { get; set; }
    }
}
