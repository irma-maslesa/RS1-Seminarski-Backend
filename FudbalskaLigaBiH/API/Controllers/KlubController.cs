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
        public KlubController(IKlubService service) : base(service)
        {
        }
    }
}
