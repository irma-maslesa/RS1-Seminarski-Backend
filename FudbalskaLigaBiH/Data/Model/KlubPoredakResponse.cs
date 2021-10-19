using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class KlubPoredakResponse
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Slika { get; set; }

        public int OdigraneUtakmice { get; set; }
        public int Pobjede { get; set; }
        public int Porazi { get; set; }
        public int Remi { get; set; }
        public int PostignutiGolovi { get; set; }
        public int PrimljeniGolovi { get; set; }
        public int Bodovi { get; set; }

        public List<UtakmicaSimpleResponse> prethodneUtakmice { get; set; }
        public UtakmicaSimpleResponse iducaUtakmica{ get; set; }
    }
}
