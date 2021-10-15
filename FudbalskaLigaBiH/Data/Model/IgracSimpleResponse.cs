using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class IgracSimpleResponse
    {
        public int ID { get; set; }
        public string ImePrezime { get; set; }
        public string Pozicija { get; set; }
        public string Klub { get; set; }
        public int BrojDresa { get; set; }
    }
}
