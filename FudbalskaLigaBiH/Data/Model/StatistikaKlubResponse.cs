using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StatistikaKlubResponse
    {
        public int ID { get; set; }
        public int Posjed { get; set; }
        public int Sutevi { get; set; }
        public int SuteviOkvir { get; set; }
        public int SuteviVanOkvira { get; set; }
        public int SuteviBlokirani { get; set; }
        public int Korneri { get; set; }
        public int Ofsajdi { get; set; }
        public int Odbrane { get; set; }
        public int Prekrsaji { get; set; }
        public int Dodavanja { get; set; }
        public int Uklizavanja { get; set; }
        public int ZutiKarton { get; set; }
        public int Napadi { get; set; }
        public int NapadiOpasni { get; set; }

        public int KlubId { get; set; }
        public string KlubNaziv { get; set; }
        public string KlubSlika { get; set; }
    }
}
