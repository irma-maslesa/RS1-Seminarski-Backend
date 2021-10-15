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
    public class IgracService : ReadService<Model.IgracSimpleResponse, Entity.Igrac, Model.IgracSearchRequest>, IIgracService
    {
        public IgracService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IList<Model.IgracSimpleResponse> get(Model.IgracSearchRequest search)
        {
            IQueryable<Entity.Igrac> entitySet = context.Igrac.Include(e => e.Klub).Include(e => e.Pozicija).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.ImePrezime))
            {
                entitySet = entitySet.Where(e => (e.Ime.ToLower() + e.Prezime.ToLower()).Contains(search.ImePrezime.ToLower()));
            }

            if (search?.Klub != null)
            {
                entitySet = entitySet.Where(e => e.Klub.Naziv.ToLower().Contains(search.Klub.ToLower()));
            }
            if (search?.PozicijaID != null)
            {
                entitySet = entitySet.Where(e => e.Pozicija.PozicijaID == search.PozicijaID);
            }
            if (search?.GradID != null)
            {
                entitySet = entitySet.Where(e => e.Grad.ID == search.GradID);
            }
            if (search?.KlubID != null)
            {
                entitySet = entitySet.Where(e => e.Klub.ID == search.KlubID);
            }

            List<Entity.Igrac> entityList = entitySet.OrderBy(e => e.BrojDresa).ToList();

            return mapper.Map<List<Model.IgracSimpleResponse>>(entityList);
        }

        public IList<Model.IgracResponse> getByKlub(int klubId)
        {
            List<Entity.Igrac> entityList = context.Igrac.Include(e=> e.Pozicija).Where(e => e.Klub.ID == klubId).ToList();
            List<Model.IgracResponse> response = mapper.Map<List<Model.IgracResponse>>(entityList);

            List<int> klubUtakmiceIds = context.StatistikaKlub.Include(e => e.Klub).Include(e => e.Utakmica).Where(e => e.Klub.ID == klubId).Select(e => e.Utakmica.UtakmicaID).ToList();
            foreach (var igrac in response)
            {
                var statistike = context.StatistikaIgrac.Include(e => e.Igrac).Where(e => e.Igrac.IgracID == igrac.ID && klubUtakmiceIds.Contains(e.Utakmica.UtakmicaID)).ToList();
                igrac.OdigraneUtakmice = statistike.FindAll(e => e.BrojMinuta > 0).Count;
                igrac.Golovi = statistike.Sum(e => e.Golovi); 
                igrac.Asistencije = statistike.Sum(e => e.Asistencije); 
                igrac.ZutiKarton = statistike.Sum(e => e.ZutiKarton); 
                igrac.CrveniKarton = statistike.Sum(e => e.CrveniKarton);
            }

            return response.OrderBy(e => e.BrojDresa).ToList();
        }

        public override Model.IgracSimpleResponse getById(int id)
        {
            Entity.Igrac entity = context.Set<Entity.Igrac>()
                .Include(e => e.Klub)
                .Include(e => e.Pozicija)
                .Where(e => e.IgracID == id)
                .SingleOrDefault();

            if (entity == null)
                throw new UserException($"{typeof(Entity.Igrac).Name}({id}) ne postoji!");

            return mapper.Map<Model.IgracSimpleResponse>(entity);
        }
    }
}
