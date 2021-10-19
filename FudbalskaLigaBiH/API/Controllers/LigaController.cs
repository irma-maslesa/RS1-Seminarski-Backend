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
    public class LigaController : CRUDController<Model.LigaResponse, object, Model.LigaUpsertRequest, Model.LigaUpsertRequest>
    {
        public LigaController(ILigaService service) : base(service)
        {
        }
    }
}
