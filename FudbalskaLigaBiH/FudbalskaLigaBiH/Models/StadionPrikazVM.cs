using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class StadionPrikazVM
    {
        public class Row
        {
            public int ID { get; set; }
            public string Naziv { get; set; }
            public int Kapacitet { get; set; }
            public string Grad { get; set; }
        }

        public List<Row> Stadioni;
        public string filter { get; set; }
    }
}
