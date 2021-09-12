using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FudbalskaLigaBiH.Models
{
    public class UtakmicaPrikazVM
    {
        public class Row
        {
            public int UtakmicaID { get; set; }

            public string KlubDomacin { get; set; }
            public string KlubGost { get; set; }

            public string slikaDomacin { get; set; }
            public string slikaGost { get; set; }

            public int RezultatDomacin { get; set; }
            public int RezultatGost { get; set; }

            public bool IsZavrsena { get; set; }
            public bool IsProduzeci { get; set; }
            public int MinutaIgre { get; set; }
            public bool IsPoluvrijeme { get; set; }
            public int LigaID { get; set; }
        }
        public List<Row> listaUtakmica { get; set; }

        public int KlubID { get; set; }
        public List<SelectListItem> listaKlubova { get; set; }
        public int LigaID { get; set; }
    }
}
