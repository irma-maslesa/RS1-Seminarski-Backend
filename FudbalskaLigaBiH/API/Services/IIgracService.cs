using Data.Model;
using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IIgracService : IReadService<Model.IgracSimpleResponse, Model.IgracSearchRequest>
    {
        public IList<Model.IgracResponse> getByKlub(int klubId);
    }
}
