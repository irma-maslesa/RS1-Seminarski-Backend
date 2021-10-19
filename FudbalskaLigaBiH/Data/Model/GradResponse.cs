using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class GradResponse
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public LoV Entitet { get; set; }
        //public List<IgracResponse> Igraci { get; set; }
    }
}
