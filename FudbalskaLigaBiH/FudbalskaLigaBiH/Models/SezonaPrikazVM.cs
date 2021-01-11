using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class SezonaPrikazVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
            public string DatumPocetka { get; set; }
            public string DatumZavrsetka { get; set; }
        }

        public List<Row> Sezone { get; set; }
        public string NazivLige { get; set; }
    }
}
