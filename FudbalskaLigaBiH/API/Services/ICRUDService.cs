using System.Collections.Generic;
using Model = Data.Model;
using Entity = Data.EntityModel;

namespace API.Services
{
    public interface ICRUDService<Response, Search, Insert, Update> : IReadService<Response, Search> 
        where Response : class
        where Search : class
        where Insert : class
        where Update : class
    {
        public Response insert(Insert request);
        public Response update(int id, Update request);
        public void delete(int id);
    }
}
