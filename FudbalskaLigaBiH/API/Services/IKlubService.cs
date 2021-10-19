using Data.EntityModel;
using Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = Data.Model;

namespace API.Services
{
    public interface IKlubService : ICRUDService<Model.KlubResponse, Model.KlubSearchRequest, Model.KlubUpsertRequest, Model.KlubUpsertRequest>
    {
        IList<LoV> getLoVs();
        IList<KlubPoredakResponse> getPoredak(int klubId = 0, int ligaId = 0);
    }
}
