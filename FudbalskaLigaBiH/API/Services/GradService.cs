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
    public class GradService :
        CRUDService<Model.GradResponse, Entity.Grad, Model.GradSearchRequest, Model.GradUpsertRequest, Model.GradUpsertRequest>,
        IGradService
    {
        public GradService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.GradResponse> get(Model.GradSearchRequest search)
        {
            IQueryable<Entity.Grad> entitySet = context.Grad.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entitySet = entitySet.Where(e => e.Naziv.ToLower().Contains(search.Naziv.ToLower()));
            }
            if (search?.EntitetId != null)
            {
                entitySet = entitySet.Where(e => e.Entitet.ID == search.EntitetId);
            }
            if (search?.IgracId != null)
            {
                entitySet = entitySet.Where(e => e.Igraci.Select(i => i.IgracID).Contains(search.IgracId.Value));
            }

            List<Entity.Grad> entityList = entitySet.Include(e => e.Entitet).ToList();

            return mapper.Map<List<Model.GradResponse>>(entityList);
        }

        public override Model.GradResponse getById(int id)
        {
            Entity.Grad entity = context.Grad.Include(e => e.Entitet).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Grad({id}) ne postoji!");

            return mapper.Map<Model.GradResponse>(entity);
        }

        public override Model.GradResponse insert(Model.GradUpsertRequest request)
        {
            Entity.Entitet entietEntity = context.Entitet.Find(request.EntitetId);
            if (entietEntity == null)
                throw new UserException($"Entitet({request.EntitetId}) ne postoji!");

            Entity.Grad entity = mapper.Map<Model.GradUpsertRequest, Entity.Grad>(request);

            entity = context.Grad.Add(entity).Entity;

            context.SaveChanges();

            Entity.Grad responseEntity = context.Grad.Include(e => e.Entitet).FirstOrDefault(e => e.ID == entity.ID);
            return mapper.Map<Entity.Grad, Model.GradResponse>(responseEntity);
        }

        public override Model.GradResponse update(int id, Model.GradUpsertRequest request)
        {
            Entity.Entitet entietEntity = context.Entitet.Find(request.EntitetId);
            if (entietEntity == null)
                throw new UserException($"Entitet({request.EntitetId}) ne postoji!");

            Entity.Grad entity = context.Grad.FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Grad({id}) ne postoji!");

            entity = mapper.Map(request, entity);

            context.Grad.Update(entity);
            context.SaveChanges();

            Entity.Grad responseEntity = context.Grad.Include(e => e.Entitet).FirstOrDefault(e => e.ID == id);

            return mapper.Map<Entity.Grad, Model.GradResponse>(responseEntity);
        }
    }
}
