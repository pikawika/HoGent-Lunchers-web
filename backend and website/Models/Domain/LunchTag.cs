using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lunchers.Models.Domain
{
    public class LunchTag
    {
        public int LunchId { get; set; }
        public Lunch Lunch { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
