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
    [ApiExplorerSettings(GroupName = "stadion-api")]
    public class StadionController : CRUDController<Model.StadionResponse, Model.StadionSearchRequest, Model.StadionUpsertRequest, Model.StadionUpsertRequest>
    {
        public StadionController(IStadionService service) : base(service)
        {
        }
    }
}
