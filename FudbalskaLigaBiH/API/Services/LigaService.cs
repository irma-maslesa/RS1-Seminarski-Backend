using AutoMapper;
using Data;
using System.Collections.Generic;
using Model = Data.Model;
using Entity = Data.EntityModel;
using FudbalskaLigaBiH.API.Filter;
using System.Linq;

namespace API.Services
{
    public class LigaService : CRUDService<Model.LigaResponse, Entity.Liga, object, Model.LigaUpsertRequest, Model.LigaUpsertRequest>, ILigaService
    {
        public LigaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
