using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.EntityModel
{
    public class Korisnik:IdentityUser
    {
        public string Ime{ get; set; }
        public string Prezime{ get; set; }
        public int brojNotifikacija{ get; set; }

    }
}
