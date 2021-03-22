using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rep
{
    public class Class1r
    {
        public string naslov { get; set; }
        public string sadrzaj { get; set; }
        public DateTime datum { get; set; }

        public static List<Class1r>Get()
        {
            return new List<Class1r> { };
        }
    }
}