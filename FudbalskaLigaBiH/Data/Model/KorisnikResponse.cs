using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class KorisnikResponse
    {
        public string Id { get; set; }
        public string Ime{ get; set; }
        public string Prezime{ get; set; }
        public string Uloga { get; set; }

        public bool ZapamtiMe { get; set; }

        public string Token { get; set; }

    }
}
