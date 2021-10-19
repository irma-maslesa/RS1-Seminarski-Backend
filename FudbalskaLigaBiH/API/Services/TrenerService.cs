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
        public override void delete(int id)
        {
            Entity.Trener entity = context.Set<Entity.Trener>().Find(id);

            if (entity == null)
                throw new UserException($"{typeof(Entity.Trener).Name}({id}) ne postoji!");

            context.Set<Entity.Trener>().Remove(entity);


            Entity.Klub klubEntity = context.Klub.Include(e => e.Trener).Where(e => e.Trener.ID == id).FirstOrDefault();

            if (klubEntity != null)
            {
                klubEntity.Trener = null;
                klubEntity.TrenerID = null;
                context.Update(klubEntity);
            }


            context.SaveChanges();
        }

        public IList<Model.TrenerResponse> getAvailable()
        {
            List<Entity.Trener> entityList = context.Set<Entity.Trener>().Where(e => e.Klub == null).ToList();

            return mapper.Map<List<Model.TrenerResponse>>(entityList);
        }

    }
}
