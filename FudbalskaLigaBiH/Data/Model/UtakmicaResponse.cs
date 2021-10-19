using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class UtakmicaResponse
    {
        public int ID { get; set; }
        public DateTime DatumOdrzavanja { get; set; }
        public int RezultatDomacin { get; set; }
        public int RezultatGost { get; set; }

        public int KlubDomacinID { get; set; }
        public string KlubDomacinNaziv { get; set; }
        public string KlubDomacinSlika { get; set; }

        public int KlubGostID { get; set; }
        public string KlubGostNaziv { get; set; }
        public string KlubGostSlika { get; set; }

        public bool IsZavrsena { get; set; }
        public bool IsProduzeci { get; set; }
        public bool IsPoluvrijeme { get; set; }
        public int MinutaIgre { get; set; }

        public LigaResponse Liga { get; set; }
    }
}
