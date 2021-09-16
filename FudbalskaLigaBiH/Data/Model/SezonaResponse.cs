using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class SezonaResponse
    {
        public int ID { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }

        public LigaResponse Liga { get; set; }

        //public List<UtakmicaResponse> utakmice { get; set; }
    }
}
