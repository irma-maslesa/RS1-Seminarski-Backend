using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class KlubDodajUrediVM
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }
        public int? TrenerID { get; set; }
        public int LigaID { get; set; }
        public int StadionID { get; set; }

        public List<SelectListItem> Treneri { get; set; }
        public List<SelectListItem> Stadioni { get; set; }
        public List<SelectListItem> Lige { get; set; }
    }
}
