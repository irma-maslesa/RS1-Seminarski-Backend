using Model = Data.Model;

namespace API.Services
{
    public interface ISezonaService : ICRUDService<Model.SezonaResponse, Model.SezonaSearchRequest, Model.SezonaUpsertRequest, Model.SezonaUpsertRequest>
    {
    }
}
