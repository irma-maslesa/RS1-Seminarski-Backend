using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface ITrenerService : ICRUDService<Model.TrenerResponse, Model.TrenerSearchRequest, Model.TrenerUpsertRequest, Model.TrenerUpsertRequest>
    {

        public IList<Model.TrenerResponse> getAvailable();
    }
}
