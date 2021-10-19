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
    [ApiExplorerSettings(GroupName = "klub-api")]
    public class KlubController : CRUDController<Model.KlubResponse, Model.KlubSearchRequest, Model.KlubUpsertRequest, Model.KlubUpsertRequest>
    {
        IKlubService service;
        public KlubController(IKlubService service) : base(service)
        {
            this.service = service;
        }

        [HttpPost]
        public override Model.KlubResponse insert([FromForm] Model.KlubUpsertRequest trener)
        {
            return service.insert(trener);
        }

        [HttpPut("{id}")]
        public override Model.KlubResponse update(int id, [FromForm] Model.KlubUpsertRequest trener)
        {
            return service.update(id, trener);
        }

        [HttpGet]
        [Route("poredak")]
        public IList<Model.KlubPoredakResponse> getPoredak([FromQuery] int klubId= 0, [FromQuery] int ligaId= 0)
        {
            if(klubId != 0)
                return service.getPoredak(klubId: klubId);
            else
                return service.getPoredak(ligaId: ligaId);
        }

        [HttpGet]
        [Route("lov")]
        public IList<Model.LoV> getLoV()
        {
            return service.getLoVs();
        }

    }
}
