using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class IgracUrediVM
    {
        public int ID { get; set; }
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        [Required]
        public int Visina { get; set; }
        [Required]
        public int Tezina { get; set; }
        [Required]
        public int BrojDresa { get; set; }

        public int GradID { get; set; }

        public int PozicijaID { get; set; }

        public int KlubID { get; set; }
        public IFormFile SlikaIgraca { get; set; }
        public string slika { get; set; }

        public List<SelectListItem> gradovi { get; set; }
        public List<SelectListItem> pozicije { get; set; }
        public List<SelectListItem> klubovi { get; set; }
    }
}

