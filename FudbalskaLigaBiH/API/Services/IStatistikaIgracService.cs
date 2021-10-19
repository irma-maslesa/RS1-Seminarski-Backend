using Data.Model;
using FudbalskaLigaBiH.API.Filter;
using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IStatistikaIgracService : ICRUDService<Model.StatistikaIgracResponse, object, Model.StatistikaIgracInsertRequest, object>
    {
        List<StatistikaIgracSezonaResponse> getSezonaStatistikaByIgrac(int igracId);
        public List<StatistikaIgracUtakmicaResponse> getUtakmicaStatistikaByIgrac(int igracId, PaginationFilter filter);
    }
}
