using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class KorisnikUtakmica
    {
        public int Id { get; set; }


        public string KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public int UtakmicaID { get; set; }
        public Utakmica Utakmica { get; set; }
    }
}
