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
    public class UtakmicaService :
        ReadService<Model.UtakmicaResponse, Entity.Utakmica, Model.UtakmicaSearchRequest>,
        IUtakmicaService
    {
        public UtakmicaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.UtakmicaResponse> get(Model.UtakmicaSearchRequest search)
        {
            IQueryable<Entity.Utakmica> entitySet = context.Utakmica.AsQueryable();

            if (search?.Status != null)
            {
                if (search.Status.Equals("ZAVRSENA"))
                {
                    entitySet = entitySet.Where(e => e.IsZavrsena);
                }
                else if (search.Status.Equals("U TOKU"))
                {
                    entitySet = entitySet.Where(e => !e.IsZavrsena && e.MinutaIgre != -1);
                }
                else if (search.Status.Equals("PREDSTOJECA"))
                {
                    entitySet = entitySet.Where(e => e.MinutaIgre == -1 && !e.IsZavrsena);
                }
            }
            if (search?.StadionId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.Stadion.ID == search.StadionId);
            }
            
            
            if (search?.KlubId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.ID == search.KlubId || e.KlubGost.ID == search.KlubId);
            }
            if (search?.KlubDomacinId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.ID == search.KlubDomacinId);
            }
            if (search?.KlubGostId != null)
            {
                entitySet = entitySet.Where(e => e.KlubGost.ID == search.KlubGostId);
            }
            if (search?.SezonaIds != null)
            {
                entitySet = entitySet.Where(e => search.SezonaIds.Contains(e.Sezona.ID));
            }

            List<Entity.Utakmica> entityList = entitySet.Include(e => e.Liga).Include(e => e.KlubDomacin).Include(e => e.KlubGost).OrderByDescending(e => e.DatumOdrzavanja).ToList();

            return mapper.Map<List<Model.UtakmicaResponse>>(entityList);
        }
    }
}
