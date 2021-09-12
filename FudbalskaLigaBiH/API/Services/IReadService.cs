using System.Collections.Generic;
using Model = Data.Model;
using Entity = Data.EntityModel;

namespace API.Services
{
    public interface IReadService<Response, Search>
        where Response : class
        where Search : class
    {
        public IList<Response> get(Search search = null);
        public Response getById(int id);
    }
}
