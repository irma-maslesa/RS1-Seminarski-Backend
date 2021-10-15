using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class UtakmicaSimpleResponse
    {
        public int ID { get; set; }
        public DateTime DatumOdrzavanja { get; set; }

        public int RezultatDomacin { get; set; }
        public int RezultatGost { get; set; }

        public int KlubDomacinID { get; set; }
        public string KlubDomacinNaziv { get; set; }

        public int KlubGostID { get; set; }
        public string KlubGostNaziv { get; set; }


    }
}
