using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class IgracSearchRequest
    {
        public string ImePrezime { get; set; }

        public int? GradID { get; set; }

        public int? PozicijaID { get; set; }

        public int? KlubID { get; set; }
        public string Klub { get; set; }

    }
}
