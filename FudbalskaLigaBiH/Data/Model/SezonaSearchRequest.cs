using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class SezonaSearchRequest
    {
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }

        public int? LigaId { get; set; }

        //public List<int> utakmiceIds { get; set; }
    }
}
