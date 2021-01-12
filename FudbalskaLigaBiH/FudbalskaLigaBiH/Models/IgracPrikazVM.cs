﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
            public DateTime DatumRodjenja { get; set; }
            public string Email { get; set; }
            public int Visina { get; set; }
            public int Tezina { get; set; }
            public int BrojDresa { get; set; }

            public int GradID { get; set; }
            public string Grad { get; set; }

            public int PozicijaID { get; set; }
            public string Pozicija { get; set; }

            public string KlubNaziv { get; set; }
        }
        public List<IgracRow> ListaIgraca { get; set; }
        public List<SelectListItem> gradovi { get; set; }
        public List<SelectListItem> pozicije { get; set; }
    }
}
