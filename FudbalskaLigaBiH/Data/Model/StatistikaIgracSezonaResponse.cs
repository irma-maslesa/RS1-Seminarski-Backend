using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaIgracSezonaResponse
    {
        public string Sezona { get; set; }
        public int OdigraneUtakmice { get; set; }
        public int Golovi { get; set; }
        public int ZutiKarton { get; set; }
        public int CrveniKarton { get; set; }
        public double BrojMinuta { get; set; }
        public int Asistencije { get; set; }
    }
}
