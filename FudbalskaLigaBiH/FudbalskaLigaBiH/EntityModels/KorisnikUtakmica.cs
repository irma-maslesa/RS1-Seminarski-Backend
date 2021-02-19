using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.EntityModels
{
    public class KorisnikUtakmica
    {
        public int Id { get; set; }
        
        public Korisnik Korisnik { get; set; }

        public int UtakmicaID { get; set; }
        public Utakmica Utakmica { get; set; }
    }
}
