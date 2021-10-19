using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Model
{
    public class KorisnikLoginRequest
    {
        public string Email{ get; set; }
        public string Password{ get; set; }
        public bool RememberMe { get; set; }

    }
}
