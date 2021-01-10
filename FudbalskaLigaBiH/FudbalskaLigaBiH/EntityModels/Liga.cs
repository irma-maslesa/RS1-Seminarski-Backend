using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.EntityModels
{
    public class Liga
    {
        public int ID { get; set; }
        public string Naziv { get; set; }

        public List<Sezona> Sezona { get; set; }

        //public List<Klub> Klubovi { get; set; }
    }
}
