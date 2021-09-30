using Model = Data.Model;

namespace API.Services
{
    public interface IUtakmicaService : IReadService<Model.UtakmicaResponse, Model.UtakmicaSearchRequest>
    {
    }
}
