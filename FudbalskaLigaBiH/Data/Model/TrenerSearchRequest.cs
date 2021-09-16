using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Model
{
    public class TrenerSearchRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Mail { get; set; }
        public DateTime? DatumRodjenja { get; set; }
    }
}
