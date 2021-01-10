using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class LigaPrikazVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
        }

        public List<Row> Lige { get; set; }
    }
}
