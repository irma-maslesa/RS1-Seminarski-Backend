using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Igrac
    {
        public int IgracID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        public int Visina { get; set; }
        public int Tezina { get; set; }
        public int BrojDresa { get; set; }

        public int GradID { get; set; }
        public Grad Grad { get; set; }

        public int PozicijaID { get; set; }
        public Pozicija Pozicija { get; set; }

        public int? KlubID { get; set; }
        public Klub Klub { get; set; }
        public string Slika { get; set; }

        public List<StatistikaIgrac> Statistika { get; set; }
    }
}
