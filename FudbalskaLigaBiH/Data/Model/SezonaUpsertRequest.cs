using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class SezonaUpsertRequest
    {
        [Required]
        public DateTime DatumPocetka { get; set; }
        [Required]
        public DateTime DatumZavrsetka { get; set; }

        [Required]
        public int LigaId { get; set; }
    }
}
