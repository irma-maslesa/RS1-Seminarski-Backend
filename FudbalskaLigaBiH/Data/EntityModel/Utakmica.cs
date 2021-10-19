using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Utakmica
    {
        public int UtakmicaID { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int RezultatDomacin { get; set; }
        public int RezultatGost { get; set; }

        public int KlubDomacinID { get; set; }
        public Klub KlubDomacin { get; set; }

        public int KlubGostID { get; set; }
        public Klub KlubGost { get; set; }

        public bool IsZavrsena { get; set; }
        public bool IsProduzeci { get; set; }
        public bool IsPoluvrijeme { get; set; }
        public int MinutaIgre { get; set; }

        public int LigaID { get; set; }
        public Liga Liga { get; set; }

        public int? SezonaID { get; set; }
        public Sezona Sezona { get; set; }

    }
}
