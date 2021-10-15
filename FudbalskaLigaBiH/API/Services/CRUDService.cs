using AutoMapper;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = Data.Model;
using Entity = Data.EntityModel;
using FudbalskaLigaBiH.API.Filter;

namespace API.Services
{
    public class CRUDService<Response, Entity, Search, Insert, Update> :
        ReadService<Response, Entity, Search>,
        ICRUDService<Response, Search, Insert, Update>
        where Response : class
        where Entity : class
        where Search : class
        where Insert : class
        where Update : class
    {
        public ApplicationDbContext context { get; set; }
        protected readonly IMapper mapper;

        public CRUDService(ApplicationDbContext context, IMapper mapper):base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual Response insert(Insert request)
        {
            Entity entity = mapper.Map<Insert, Entity>(request);
            entity = context.Set<Entity>().Add(entity).Entity;

            context.SaveChanges();

            return mapper.Map<Entity, Response>(entity);
        }

        public virtual Response update(int id, Update request)
        {
            Entity entity = context.Set<Entity>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity).Name}({id}) ne postoji!");

            entity = mapper.Map(request, entity);

            context.Set<Entity>().Update(entity);
            context.SaveChanges();

            return mapper.Map<Entity, Response>(entity);
        }

        public virtual void delete(int id)
        {
            Entity entity = context.Set<Entity>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity).Name}({id}) ne postoji!");

            context.Set<Entity>().Remove(entity);
            context.SaveChanges();
        }
    }
}
