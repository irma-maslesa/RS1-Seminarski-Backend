using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class KlubPrikazVM
    {
        public class Row
        {

            public int ID { get; set; }
            public string Naziv { get; set; }
            public string Mail { get; set; }
            public string Adresa { get; set; }
            public string Trener { get; set; }
            public string Liga { get; set; }
            public string Stadion { get; set; }
        }

        public List<Row> Klubovi { get; set; }
    }
}
