using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Entity = Data.EntityModel;
using Model = Data.Model;

namespace API.Controllers
{
    [ApiExplorerSettings(GroupName = "account-api")]
    public class AccountController : ReadController<Model.KorisnikResponse, object>
    {
        public IAccountService service { get; set; }
        public AccountController(IAccountService service) : base(service)
        {
            this.service = service;
        }

        [HttpPost("prijava")]
        public Task<Model.KorisnikResponse> login([FromBody] Model.KorisnikLoginRequest korisnik)
        {
            return service.login(korisnik);
        }

        [HttpPost("odjava")]
        public Task<string> logout()
        {
            return service.logout();
        }

        [HttpGet("{id}/omiljene")]
        public List<Model.UtakmicaResponse> getOmiljeneUtakmice(string id)
        {
            return service.getOmiljeneUtakmice(id);
        }

        [HttpPost("omiljene")]
        public List<Model.UtakmicaResponse> setOmiljenaUtakmica([FromBody] Model.OmiljenaUtakmicaRequest request)
        {
            return service.setOmiljenaUtakmica(request);
        }

        [HttpDelete("omiljene")]
        public List<Model.UtakmicaResponse> removeOmiljenaUtakmica([FromQuery] string korisnikId, [FromQuery] int utakmicaId)
        {
            Model.OmiljenaUtakmicaRequest request = new Model.OmiljenaUtakmicaRequest { KorisnikId = korisnikId, UtakmicaID = utakmicaId };
            return service.removeOmiljenaUtakmica(request);
        }
    }
}
