using Model = Data.Model;

namespace API.Services
{
    public interface IGradService : ICRUDService<Model.GradResponse, Model.GradSearchRequest, Model.GradUpsertRequest, Model.GradUpsertRequest>
    {
    }
}
