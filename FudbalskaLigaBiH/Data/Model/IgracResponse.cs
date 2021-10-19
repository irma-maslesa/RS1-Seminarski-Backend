using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class IgracResponse
    {
        public int ID { get; set; }
        public int BrojDresa { get; set; }
        public string ImePrezime { get; set; }
        public string Pozicija { get; set; }
        public int Godine { get; set; }
        public int OdigraneUtakmice { get; set; }
        public int Golovi { get; set; }
        public int Asistencije { get; set; }
        public int ZutiKarton { get; set; }
        public int CrveniKarton { get; set; }
    }
}
