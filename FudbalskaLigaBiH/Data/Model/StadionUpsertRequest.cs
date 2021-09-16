using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class StadionUpsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Naziv { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Minimalna vrijednost polja kapacitet je {1}!")]
        public int Kapacitet { get; set; }

        [Required]
        public int GradId { get; set; }
    }
}
