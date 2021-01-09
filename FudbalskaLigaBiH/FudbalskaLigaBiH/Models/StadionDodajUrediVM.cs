using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class StadionDodajUrediVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Kapacitet { get; set; }
        public int GradID { get; set; }

        public List<SelectListItem> Gradovi { get; set; }
    }
}
