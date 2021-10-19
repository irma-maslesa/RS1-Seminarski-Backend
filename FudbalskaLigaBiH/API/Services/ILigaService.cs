using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface ILigaService : ICRUDService<Model.LigaResponse, object, Model.LigaUpsertRequest, Model.LigaUpsertRequest>
    {
    }
}
