using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Data.Model
{
    public class KlubUpsertRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Naziv { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Mail { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Adresa { get; set; }


        public IFormFile Slika { get; set; }

        public List<int> IgraciIds { get; set; }

        public int? TrenerId { get; set; }

        [Required]
        public int StadionId { get; set; }

        [Required]
        public int LigaId { get; set; }
    }
}
