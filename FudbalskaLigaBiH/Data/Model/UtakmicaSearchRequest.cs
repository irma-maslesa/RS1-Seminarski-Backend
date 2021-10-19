using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class UtakmicaSearchRequest
    {
        public string? Status { get; set; }

        public bool? Analitika { get; set; }
        public int? StadionId { get; set; }

        public int? KlubId { get; set; }

        public int? KlubDomacinId { get; set; }
        public int? KlubGostId { get; set; }
        public List<int> SezonaIds { get; set; }
    }
}
