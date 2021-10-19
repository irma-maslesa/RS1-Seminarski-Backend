using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaKlubSezonaResponse
    {
        public string Sezona { get; set; }

        public int OdigraneUtakmice { get; set; }

        public int Pobjeda { get; set; }
        public int Poraz { get; set; }

        public int Remi { get; set; }

        public int PostignutiGolovi { get; set; }
        public int PrimljeniGolovi { get; set; }

    }
}
