using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FudbalskaLigaBiH.Models
{
    public class SezonaDodajUrediVM
    {
        public int ID { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public int LigaID { get; set; }

        public List<SelectListItem> Lige { get; set; }
    }
}
