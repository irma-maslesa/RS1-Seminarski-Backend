using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class IgracPrikazVM
    {
        public class IgracRow
        {
            public int ID { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public int BrojDresa{ get; set; }
            public string Pozicija { get; set; }
            public string KlubNaziv { get; set; }
  
        }
        public List<IgracRow> ListaIgraca { get; set; }
        public int KlubID { get; set; }
        public List<SelectListItem> klubovi { get; set; }
        public string pretraga { get; set; }
    }
}
