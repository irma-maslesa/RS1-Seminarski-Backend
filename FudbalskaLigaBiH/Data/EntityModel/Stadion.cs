using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Stadion
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }

        public int GradID { get; set; }
        public Grad Grad { get; set; }

        public Klub Klub { get; set; }
    }
}
