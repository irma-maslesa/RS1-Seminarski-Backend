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
    public class ReadService<Response, Entity, Search> :
        IReadService<Response, Search>
        where Response : class
        where Entity : class
        where Search : class
    {
        public ApplicationDbContext context { get; set; }
        protected readonly IMapper mapper;

        public ReadService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual IList<Response> get(Search search)
        {
            List<Entity> entityList = context.Set<Entity>().ToList();

            return mapper.Map<List<Response>>(entityList);
        }

        public virtual Response getById(int id)
        {
            Entity entity = context.Set<Entity>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity).Name}({id}) ne postoji!");

            return mapper.Map<Response>(entity);
        }
    }
}
