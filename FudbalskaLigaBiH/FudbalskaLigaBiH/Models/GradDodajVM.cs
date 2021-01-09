using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class GradDodajVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int EntitetID { get; set; }

        public List<SelectListItem> Entiteti { get; set; }
    }
}
