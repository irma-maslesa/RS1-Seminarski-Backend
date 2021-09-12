using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StadionResponse
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }

        public GradResponse Grad { get; set; }

        //public KlubResponse Klub { get; set; }
    }
}
