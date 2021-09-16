using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class KlubResponse
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }

        //trebas ubaciti slike za klub
        public string Slika { get; set; }

        public int? TrenerID { get; set; }
        public string TrenerImePrezime { get; set; }

        public int? StadionID { get; set; }
        public string StadionNaziv { get; set; }

        public LigaResponse Liga { get; set; }
    }
}
