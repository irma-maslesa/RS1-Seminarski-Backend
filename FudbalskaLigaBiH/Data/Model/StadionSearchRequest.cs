using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StadionSearchRequest
    {
        public string Naziv { get; set; }
        public int? Kapacitet { get; set; }

        public int? GradId { get; set; }

        public int? KlubId { get; set; }
    }
}
