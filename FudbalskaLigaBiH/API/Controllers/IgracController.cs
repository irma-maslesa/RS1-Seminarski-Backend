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
    [ApiExplorerSettings(GroupName = "igrac-api")]
    public class IgracController : ReadController<Model.IgracSimpleResponse, Model.IgracSearchRequest>
    {
        protected readonly IIgracService service;
        public IgracController(IIgracService service):base(service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("get-by-klub")]
        public virtual IList<Model.IgracResponse> getByKlub([FromQuery] int klubId)
        {
            return service.getByKlub(klubId);
        }
    }
}
