using AutoMapper;
using Data;
using System.Collections.Generic;
using Model = Data.Model;
using Entity = Data.EntityModel;
using FudbalskaLigaBiH.API.Filter;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class LigaService : CRUDService<Model.LigaResponse, Entity.Liga, object, Model.LigaUpsertRequest, Model.LigaUpsertRequest>, ILigaService
    {
        public LigaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override void delete(int id)
        {
            Entity.Liga entity = context.Set<Entity.Liga>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity.Liga).Name}({id}) ne postoji!");


            Entity.Klub klubEntity = context.Klub.Include(e => e.Liga).Where(e => e.Liga.ID == id).FirstOrDefault();

            if (klubEntity != null)
                throw new UserException($"Još postoje klubovi u ovoj ligi. Liga ne smije biti obrisana");


            context.Set<Entity.Liga>().Remove(entity);
            context.SaveChanges();
        }

    }
}
