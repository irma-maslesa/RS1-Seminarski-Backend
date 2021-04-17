using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class IgracPrikazVM
    {
        public int KlubID { get; set; }
        public List<SelectListItem> klubovi { get; set; }

        public int PozicijaID { get; set; }
        public List<SelectListItem> pozicije { get; set; }

        public string pretraga { get; set; }
    }
}
