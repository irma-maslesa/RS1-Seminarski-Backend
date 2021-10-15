using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadController<Response, Search> :
        ControllerBase
        where Response : class
        where Search : class
    {
        protected readonly IReadService<Response, Search> readService;

        public ReadController(IReadService<Response, Search> readService)
        {
            this.readService = readService;
        }

        [HttpGet]
        public virtual IList<Response> get([FromQuery] Search search)
        {
            return readService.get(search);
        }

        [HttpGet("{id}")]
        public virtual Response getById(int id)
        {
            return readService.getById(id);
        }
    }
}
