using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Klub
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }

        public string Slika { get; set; }

        public List<StatistikaKlub> Statistika { get; set; }
        public List<Igrac> Igraci { get; set; }

        public int? TrenerID { get; set; }
        public Trener Trener { get; set; }

        public int StadionID { get; set; }
        public Stadion Stadion { get; set; }

        public int LigaID { get; set; }
        public Liga Liga { get; set; }
    }
}
