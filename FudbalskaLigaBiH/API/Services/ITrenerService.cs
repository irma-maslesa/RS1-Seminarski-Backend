using Model = Data.Model;

namespace API.Services
{
    public interface ITrenerService : ICRUDService<Model.TrenerResponse, Model.TrenerSearchRequest, Model.TrenerUpsertRequest, Model.TrenerUpsertRequest>
    {
    }
}
