using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class StatistikaIgrac
    { 
        public int ID { get; set; }
        public int Golovi { get; set; }
        public int ZutiKarton { get; set; }
        public int CrveniKarton { get; set; }
        public int BrojMinuta { get; set; }
        public int Asistencije { get; set; }

        public int IgracId { get; set; }
        public Igrac Igrac { get; set; }

        public int UtakmicaID { get; set; }
        public Utakmica Utakmica { get; set; }
    }
}
