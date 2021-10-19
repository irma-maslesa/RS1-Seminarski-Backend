using System.Collections.Generic;
using Model = Data.Model;

namespace API.Services
{
    public interface IStatistikaService
    {
        public Model.StatistikaResponse getByUtakmicaId(int id);

        public Model.StatistikaResponse insert(Model.StatistikaInsertRequest request);
    }
}
