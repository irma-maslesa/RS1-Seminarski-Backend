using AutoMapper;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = Data.Model;
using Entity = Data.EntityModel;
using FudbalskaLigaBiH.API.Filter;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TrenerService : 
        CRUDService<Model.TrenerResponse, Entity.Trener, Model.TrenerSearchRequest, Model.TrenerUpsertRequest, Model.TrenerUpsertRequest>, 
        ITrenerService
    {
        public TrenerService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public override IList<Model.TrenerResponse> get(Model.TrenerSearchRequest search)
        {
            IQueryable<Entity.Trener> entitySet = context.Set<Entity.Trener>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Ime))
            {
                entitySet = entitySet.Where(e => e.Ime.ToLower().Contains(search.Ime.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Prezime))
            {
                entitySet = entitySet.Where(e => e.Prezime.ToLower().Contains(search.Prezime.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Mail))
            {
                entitySet = entitySet.Where(e => e.Mail.ToLower().Contains(search.Mail.ToLower()));
            }
            if (search?.DatumRodjenja != null)
            {
                entitySet = entitySet.Where(e => e.DatumRodjenja.Equals(search.DatumRodjenja));
            }

            List<Entity.Trener> entityList = entitySet.ToList();

            return mapper.Map<List<Model.TrenerResponse>>(entityList);
        }
    }
}
