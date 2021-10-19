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
    public class EntitetController : ReadController<Model.LoV, object>
    {
        public EntitetController(IEntitetService service) : base(service)
        {
        }
    }
}
