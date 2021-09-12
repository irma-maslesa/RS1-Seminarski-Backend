using Model = Data.Model;

namespace API.Services
{
    public interface IStadionService : ICRUDService<Model.StadionResponse, Model.StadionSearchRequest, Model.StadionUpsertRequest, Model.StadionUpsertRequest>
    {
    }
}
