using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Entitet
    {
        public int ID { get; set; }
        public string Naziv { get; set; }

        public List<Grad> Gradovi { get; set; }
    }
}
