using Data.EntityModel;
using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IKlubService : ICRUDService<Model.KlubResponse, Model.KlubSearchRequest, Model.KlubUpsertRequest, Model.KlubUpsertRequest>
    {
    }
}
