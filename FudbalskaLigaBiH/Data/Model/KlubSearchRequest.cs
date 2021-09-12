using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class KlubSearchRequest
    {
        public string Naziv { get; set; }
        public string Mail { get; set; }
        public string Adresa { get; set; }

        public int? TrenerId { get; set; }

        public int? StadionId { get; set; }

        public int? LigaId { get; set; }
    }
}
