using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rep
{
    public class Class1
    {
        public string naslov { get; set; }
        public string sadrzaj { get; set; }
        public DateTime datum { get; set; }

        public static List<Class1> Get()
        {
            return new List<Class1>{}; 
        }
    }
}