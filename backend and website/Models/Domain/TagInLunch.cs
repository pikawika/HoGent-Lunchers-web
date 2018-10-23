using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models
{
    public class TagInLunch
    {
        [Key]
        public int TagInLunchId { get; set; }
        public Tag Tag { get; set; }
    }
}
