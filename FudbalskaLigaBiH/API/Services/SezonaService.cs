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
    public class SezonaService :
        CRUDService<Model.SezonaResponse, Entity.Sezona, Model.SezonaSearchRequest, Model.SezonaUpsertRequest, Model.SezonaUpsertRequest>,
        ISezonaService
    {
        public SezonaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.SezonaResponse> get(Model.SezonaSearchRequest search)
        {
            IQueryable<Entity.Sezona> entitySet = context.Sezona.AsQueryable();

            if (search?.DatumPocetka != null)
            {
                entitySet = entitySet.Where(e => e.DatumPocetka <= search.DatumPocetka);
            }
            if (search?.DatumZavrsetka != null)
            {
                entitySet = entitySet.Where(e => e.DatumZavrsetka > search.DatumZavrsetka);
            }
            if (search?.LigaId != null)
            {
                entitySet = entitySet.Where(e => e.Liga.ID == search.LigaId);
            }

            List<Entity.Sezona> entityList = entitySet.Include(e => e.Liga).OrderByDescending(e => e.DatumPocetka).ToList();

            return mapper.Map<List<Model.SezonaResponse>>(entityList);
        }

        public override Model.SezonaResponse getById(int id)
        {
            Entity.Sezona entity = context.Sezona.Include(e => e.Liga).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Sezona({id}) ne postoji!");

            return mapper.Map<Model.SezonaResponse>(entity);
        }

        public override Model.SezonaResponse insert(Model.SezonaUpsertRequest request)
        {
            string errorMessage = string.Empty;

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Sezona entity = mapper.Map<Model.SezonaUpsertRequest, Entity.Sezona>(request);

            entity = context.Sezona.Add(entity).Entity;

            context.SaveChanges();

            Entity.Sezona responseEntity = context.Sezona.Include(e => e.Liga).FirstOrDefault(e => e.ID == entity.ID);
            return mapper.Map<Entity.Sezona, Model.SezonaResponse>(responseEntity);
        }

        public override Model.SezonaResponse update(int id, Model.SezonaUpsertRequest request)
        {
            string errorMessage = string.Empty;

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Sezona entity = context.Sezona.FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Sezona({id}) ne postoji! ");

            entity = mapper.Map(request, entity);

            context.Sezona.Update(entity);
            context.SaveChanges();

            Entity.Sezona responseEntity = context.Sezona.Include(e => e.Liga).FirstOrDefault(e => e.ID == id);

            return mapper.Map<Entity.Sezona, Model.SezonaResponse>(responseEntity);
        }

        public Model.SezonaResponse getTrenutnuSezonu(int ligaId)
        {
            var entity = context.Sezona.Where(e => e.Liga.ID == ligaId && e.DatumPocetka <= DateTime.Now && e.DatumZavrsetka > DateTime.Now).FirstOrDefault();

            return mapper.Map<Model.SezonaResponse>(entity);
        }
    }
}