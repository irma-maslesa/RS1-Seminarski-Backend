using FudbalskaLigaBiH.API.Filter;
using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IStatistikaKlubService : ICRUDService<Model.StatistikaKlubResponse, object, Model.StatistikaKlubInsertRequest, object>
    {
        List<Model.StatistikaKlubSezonaResponse> getSezonaStatistikaByKlub(int id);
    }
}
