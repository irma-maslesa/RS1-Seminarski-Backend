using FudbalskaLigaBiH.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class UtakmicaPrikazVM
    {
        public class Row
        {
            public int UtakmicaID { get; set; }

            public string KlubDomacin { get; set; }
            public string KlubGost { get; set; }

            //slike za grbove klubova

            public int RezultatDomacin { get; set; }
            public int RezultatGost { get; set; }

            public bool IsZavrsena { get; set; }
            public bool IsProduzeci { get; set; }
            public int MinutaIgre { get; set; }
            public bool IsOmiljena { get; set; }
        }
        public List<Row> listaUtakmica { get; set; }
        public List<Klub> listaKlubova { get; set; }

    }
}
