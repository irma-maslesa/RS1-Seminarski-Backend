using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class UtakmicaDodajUrediVM
    {
        public int UtakmicaID{ get; set; }
        public DateTime DatumOdrzavanja { get; set; }

        public int KlubDomacinID { get; set; }
        public List<SelectListItem> KlubDomacin { get; set; }

        public string slikaDomacin { get; set; }
        public string slikaGost { get; set; }


        public int KlubGostID { get; set; }
        public List<SelectListItem> KlubGost { get; set; }
        public int LigaID { get; set; }

    }
}
