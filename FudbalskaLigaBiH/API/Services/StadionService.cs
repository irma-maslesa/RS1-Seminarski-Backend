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
    public class StadionService :
        CRUDService<Model.StadionResponse, Entity.Stadion, Model.StadionSearchRequest, Model.StadionUpsertRequest, Model.StadionUpsertRequest>,
        IStadionService
    {
        public StadionService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.StadionResponse> get(Model.StadionSearchRequest search)
        {
            IQueryable<Entity.Stadion> entitySet = context.Stadion.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entitySet = entitySet.Where(e => e.Naziv.ToLower().Contains(search.Naziv.ToLower()));
            }
            if (search?.Kapacitet != null)
            {
                entitySet = entitySet.Where(e => e.Kapacitet == search.Kapacitet);
            }
            if (search?.GradId != null)
            {
                entitySet = entitySet.Where(e => e.GradID == search.GradId);
            }
            if (search?.KlubId != null)
            {
                entitySet = entitySet.Where(e => e.Klub.ID == search.KlubId);
            }

            List<Entity.Stadion> entityList = entitySet.Include(e => e.Grad).Include(e => e.Grad.Entitet).ToList();

            return mapper.Map<List<Model.StadionResponse>>(entityList);
        }

        public override Model.StadionResponse getById(int id)
        {
            Entity.Stadion entity = context.Stadion.Include(e => e.Grad).Include(e => e.Grad.Entitet).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Stadion({id}) ne postoji!");

            return mapper.Map<Model.StadionResponse>(entity);
        }

        public override Model.StadionResponse insert(Model.StadionUpsertRequest request)
        {
            string errorMessage = string.Empty;
            Entity.Grad gradEntity = context.Grad.Find(request.GradId);
            if (gradEntity == null)
                errorMessage += $"Grad({request.GradId}) ne postoji! ";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Stadion entity = mapper.Map<Model.StadionUpsertRequest, Entity.Stadion>(request);

            entity = context.Stadion.Add(entity).Entity;

            context.SaveChanges();

            Entity.Stadion responseEntity = context.Stadion.Include(e => e.Grad).Include(e => e.Grad.Entitet).FirstOrDefault(e => e.ID == entity.ID);
            return mapper.Map<Entity.Stadion, Model.StadionResponse>(responseEntity);
        }

        public override Model.StadionResponse update(int id, Model.StadionUpsertRequest request)
        {
            string errorMessage = string.Empty;
            Entity.Grad gradEntity = context.Grad.Find(request.GradId);
            if (gradEntity == null)
                errorMessage += $"Grad({request.GradId}) ne postoji! ";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Stadion entity = context.Stadion.FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Stadion({id}) ne postoji!");

            entity = mapper.Map(request, entity);

            context.Stadion.Update(entity);
            context.SaveChanges();

            Entity.Stadion responseEntity = context.Stadion.Include(e => e.Grad).Include(e => e.Grad.Entitet).FirstOrDefault(e => e.ID == id);

            return mapper.Map<Entity.Stadion, Model.StadionResponse>(responseEntity);
        }
        public override void delete(int id)
        {
            Entity.Stadion entity = context.Set<Entity.Stadion>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity.Stadion).Name}({id}) ne postoji!");

            context.Set<Entity.Stadion>().Remove(entity);

            Entity.Klub klubEntity = context.Klub.Include(e => e.Stadion).Where(e => e.Stadion.ID == id).FirstOrDefault();
            
            if(klubEntity != null)
                throw new UserException($"Klub {klubEntity.Naziv} koristi stadion. Da bi obrisali stadion promijenite stadion kluba!");

            context.SaveChanges();
        }

        public IList<Model.StadionResponse> getAvailable()
        {
            List<Entity.Stadion> entityList = context.Set<Entity.Stadion>().Where(e => e.Klub == null).Include(e=> e.Grad).ToList();

            return mapper.Map<List<Model.StadionResponse>>(entityList);
        }
    }
}
