using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Grad
    {
        public int ID { get; set; }
        public string Naziv { get; set; }

        public int EntitetID { get; set; }
        public Entitet Entitet { get; set; }

        public List<Igrac> Igraci { get; set; }
    }
}
