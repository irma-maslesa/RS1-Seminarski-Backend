using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaInsertRequest
    {
        public StatistikaKlubInsertRequest statistikaDomacin { get; set; }
        public StatistikaKlubInsertRequest statistikaGost { get; set; }
        public List<StatistikaIgracInsertRequest> statistikaDomaciIgraci { get; set; }
        public List<StatistikaIgracInsertRequest> statistikaGostujuciIgraci { get; set; }

        public int UtakmicaID { get; set; }
        public int RezultatGost { get; set; }
        public int RezultatDomacin { get; set; }

    }
}
