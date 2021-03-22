using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rep
{
    public class Class2
    {
        public int id { get; set; }
        public string naslov { get; set; }
        public int brojac { get; set; }
        public DateTime datum { get; set; }

        public static List<Class2> Get()
        {
            return new List<Class2> { };
        }
    }
}