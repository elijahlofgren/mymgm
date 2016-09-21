using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mymgm.Models
{
    public class LocalEvent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
    }
}
