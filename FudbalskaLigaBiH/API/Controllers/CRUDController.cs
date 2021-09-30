using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Services;

namespace API.Controllers
{
    public class CRUDController<Response, Search, Insert, Update> :
        ReadController<Response, Search>
        where Response : class
        where Search : class
        where Insert : class
        where Update : class
    {
        protected readonly ICRUDService<Response, Search, Insert, Update> crudService;

        public CRUDController(ICRUDService<Response, Search, Insert, Update> crudService):base(crudService)
        {
            this.crudService = crudService;
        }

        [HttpPost]
        public virtual Response insert([FromBody] Insert trener)
        {
            return crudService.insert(trener);
        }

        [HttpPut("{id}")]
        public virtual Response update(int id, Update trener)
        {
            return crudService.update(id, trener);
        }

        [HttpDelete("{id}")]
        public void delete(int id)
        {
            crudService.delete(id);
            //return crudService.delete(id);
        }
    }
}
