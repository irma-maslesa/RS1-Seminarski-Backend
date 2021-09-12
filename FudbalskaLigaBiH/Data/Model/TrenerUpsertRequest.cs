using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Model
{
    public class TrenerUpsertRequest
    {

        [Required(AllowEmptyStrings = false)]
        public string Ime { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Prezime { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Mail { get; set; }
        public DateTime DatumRodjenja { get; set; }

        //public Klub Klub { get; set; }
    }
}
