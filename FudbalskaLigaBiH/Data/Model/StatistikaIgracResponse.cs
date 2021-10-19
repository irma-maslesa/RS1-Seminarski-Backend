using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaIgracResponse
    { 
        public int ID { get; set; }
        public int Golovi { get; set; }
        public int ZutiKarton { get; set; }
        public int CrveniKarton { get; set; }
        public int BrojMinuta { get; set; }
        public int Asistencije { get; set; }

        public int IgracId { get; set; }
        public string IgracImePrezime { get; set; }
        public string IgracBrojDresa { get; set; }
    }
}
