using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Sezona
    {
        public int ID { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }

        public int LigaID { get; set; }
        public Liga Liga { get; set; }

        public List<Utakmica> utakmice { get; set; }
    }
}
