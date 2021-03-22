using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FudbalskaLigaBiH.Models
{
    public class NovostiDetaljiVM
    {
        public class NovostiRed
        {
            public int IDnovosti { get; set; }
            [Required]
            public string NaslovNovosti { get; set; }
            [Required]
            public string SadrzajNovosti { get; set; }
            public DateTime DatumObjaveNovosti { get; set; }
            public IFormFile SlikaNovosti { get; set; }
            public string slika { get; set; }
        }
        public List<NovostiRed> novosti { get; set; }
        public DateTime granica { get; set; }
    }
}
