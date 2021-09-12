using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GradSearchRequest
    {
        public string Naziv { get; set; }
        public int? EntitetId { get; set; }
        public int? IgracId { get; set; }
    }
}
