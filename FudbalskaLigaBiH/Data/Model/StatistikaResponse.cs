using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaResponse
    {
        public StatistikaKlubResponse statistikaDomacin { get; set; }
        public StatistikaKlubResponse statistikaGost { get; set; }
        public List<StatistikaIgracResponse> statistikaDomaciIgraci { get; set; }
        public List<StatistikaIgracResponse> statistikaGostujuciIgraci { get; set; }

        public int UtakmicaID { get; set; }
        public int RezultatGost { get; set; }
        public int RezultatDomacin { get; set; }

    }
}
