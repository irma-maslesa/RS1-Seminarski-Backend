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

    [ApiExplorerSettings(GroupName = "trener-api")]
    public class TrenerController : CRUDController<Model.TrenerResponse, Model.TrenerSearchRequest, Model.TrenerUpsertRequest, Model.TrenerUpsertRequest>
    {
        public ITrenerService service { get; set; }
        public TrenerController(ITrenerService service) : base(service)
        {
            this.service = service; 
        }

        [HttpGet("available")]
        public virtual IList<Model.TrenerResponse> getAvailable()
        {
            return service.getAvailable();
        }
    }
}
