using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class NovostiDetaljiVM
    {
        public class NovostiRed
        {
            public int IDnovosti { get; set; }
            public string NaslovNovosti { get; set; }
            public string SadrzajNovosti { get; set; }
            public DateTime DatumObjaveNovosti { get; set; }
        }
        public List<NovostiRed> novosti { get; set; }
    }
}
