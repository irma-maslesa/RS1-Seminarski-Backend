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
    public class KlubService :
        CRUDService<Model.KlubResponse, Entity.Klub, Model.KlubSearchRequest, Model.KlubUpsertRequest, Model.KlubUpsertRequest>,
        IKlubService
    {
        public KlubService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.KlubResponse> get(Model.KlubSearchRequest search)
        {
            IQueryable<Entity.Klub> entitySet = context.Klub.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entitySet = entitySet.Where(e => e.Naziv.ToLower().Contains(search.Naziv.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Mail))
            {
                entitySet = entitySet.Where(e => e.Mail.ToLower().Contains(search.Mail.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Adresa))
            {
                entitySet = entitySet.Where(e => e.Adresa.ToLower().Contains(search.Adresa.ToLower()));
            }
            if (search?.TrenerId != null)
            {
                entitySet = entitySet.Where(e => e.Trener.ID == search.TrenerId);
            }
            if (search?.StadionId != null)
            {
                entitySet = entitySet.Where(e => e.Stadion.ID == search.StadionId);
            }
            if (search?.LigaId != null)
            {
                entitySet = entitySet.Where(e => e.Liga.ID == search.LigaId);
            }

            List<Entity.Klub> entityList = entitySet.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).ToList();

            return mapper.Map<List<Model.KlubResponse>>(entityList);
        }

        public override Model.KlubResponse getById(int id)
        {
            Entity.Klub entity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Klub({id}) ne postoji!");

            return mapper.Map<Model.KlubResponse>(entity);
        }

        public override Model.KlubResponse insert(Model.KlubUpsertRequest request)
        {
            string errorMessage = string.Empty;
            if (request.IgraciIds != null)
            {
                foreach (int igracId in request.IgraciIds)
                {
                    Entity.Igrac igracEntity = context.Igrac.Find(igracId);
                    if (igracEntity == null)
                        errorMessage += $"Igrač({igracId}) ne postoji! ";
                    else if (igracEntity.KlubID != null)
                        errorMessage += $"Igrač({igracId}) je već dodijeljen klubu! ";
                }
            }

            Entity.Trener trenerEntity = context.Trener.Find(request.TrenerId);
            if (trenerEntity == null)
                errorMessage += $"Trener({request.TrenerId}) ne postoji! ";
            else
            {
                Entity.Klub trenerKlub = context.Klub.Where(e => e.TrenerID == request.TrenerId).FirstOrDefault();
                if (trenerKlub != null)
                    errorMessage += $"Trener({request.TrenerId}) je već dodijeljen klubu! ";
            }

            Entity.Stadion stadionEntity = context.Stadion.Find(request.StadionId);
            if (stadionEntity == null)
                errorMessage += $"Stadion({request.StadionId}) ne postoji! ";
            else if (stadionEntity.Klub != null)
                errorMessage += $"Stadion({request.StadionId}) je već dodijeljen klubu! ";

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Klub entity = mapper.Map<Model.KlubUpsertRequest, Entity.Klub>(request);

            entity = context.Klub.Add(entity).Entity;

            context.SaveChanges();

            Entity.Klub responseEntity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == entity.ID);
            return mapper.Map<Entity.Klub, Model.KlubResponse>(responseEntity);
        }

        public override Model.KlubResponse update(int id, Model.KlubUpsertRequest request)
        {
            string errorMessage = string.Empty;

            if (request.IgraciIds != null)
            {
                foreach (int igracId in request.IgraciIds)
                {
                    Entity.Igrac igracEntity = context.Igrac.Find(igracId);
                    if (igracEntity == null)
                        errorMessage += $"Igrac({igracId}) ne postoji! ";
                    else if (igracEntity.Klub != null && igracEntity.Klub.ID != id)
                        errorMessage += $"Igrač({igracId}) je već dodijeljen klubu! ";
                }
            }

            Entity.Trener trenerEntity = context.Trener.Find(request.TrenerId);
            if (trenerEntity == null)
                errorMessage += $"Trener({request.TrenerId}) ne postoji! ";
            else if (trenerEntity.Klub != null && trenerEntity.Klub.ID != id)
                errorMessage += $"Trener({request.TrenerId}) je već dodijeljen klubu! ";

            Entity.Stadion stadionEntity = context.Stadion.Find(request.StadionId);
            if (stadionEntity == null)
                errorMessage += $"Stadion({request.StadionId}) ne postoji! ";
            else if (stadionEntity.Klub != null && stadionEntity.Klub.ID != id)
                errorMessage += $"Stadion({request.StadionId}) je već dodijeljen klubu! ";

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Klub entity = context.Klub.FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Klub({id}) ne postoji! ");

            entity = mapper.Map(request, entity);

            context.Klub.Update(entity);
            context.SaveChanges();

            Entity.Klub responseEntity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == id);

            return mapper.Map<Entity.Klub, Model.KlubResponse>(responseEntity);
        }
    }
}