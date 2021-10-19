using Data.Model;
using FudbalskaLigaBiH.API.Filter;
using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IUtakmicaService : IReadService<Model.UtakmicaResponse, Model.UtakmicaSearchRequest>
    {
        IList<UtakmicaSimpleResponse> getByIgrac(int igracId, PaginationFilter filter);
        IList<UtakmicaSimpleResponse> getLast5ByKlub(int klubId, int ligaId);
        UtakmicaSimpleResponse getNextByKlub(int klubId, int ligaId);
        IList<Model.UtakmicaSimpleResponse> getByKlub(int klubId);
    }
}
