using AutoMapper;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Model = Data.Model;
using Entity = Data.EntityModel;
using FudbalskaLigaBiH.API.Filter;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Data.EntityModel;

namespace API.Services
{
    public class StatistikaKlubService :
        CRUDService<Model.StatistikaKlubResponse, Entity.StatistikaKlub, object, Model.StatistikaKlubInsertRequest, object>,
        IStatistikaKlubService
    {
        protected readonly ISezonaService sezonaService;
        protected readonly IUtakmicaService utakmicaService;
        public StatistikaKlubService(ApplicationDbContext context, IMapper mapper, ISezonaService sezonaService, IUtakmicaService utakmicaService) : base(context, mapper)
        {
            this.sezonaService = sezonaService;
            this.utakmicaService = utakmicaService;
        }

        public override IList<Model.StatistikaKlubResponse> get(object search)
        {
            List<Entity.StatistikaKlub> entityList = context.StatistikaKlub.Include(e => e.Klub).ToList();

            return mapper.Map<List<Model.StatistikaKlubResponse>>(entityList);
        }

        public override Model.StatistikaKlubResponse getById(int id)
        {
            Entity.StatistikaKlub entity = context.StatistikaKlub.Include(e => e.Klub).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Statistika Klub({id}) ne postoji!");


            return mapper.Map<Model.StatistikaKlubResponse>(entity);
        }

        public override Model.StatistikaKlubResponse insert(Model.StatistikaKlubInsertRequest request)
        {
            string errorMessage = string.Empty;

            Entity.Klub igracEntity = context.Klub.Find(request.KlubId);
            if (igracEntity == null)
                errorMessage += $"Klub({request.KlubId}) ne postoji!"; ;

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.StatistikaKlub entity = mapper.Map<Model.StatistikaKlubInsertRequest, Entity.StatistikaKlub>(request);

            entity = context.StatistikaKlub.Add(entity).Entity;

            context.SaveChanges();

            Entity.StatistikaKlub responseEntity = context.StatistikaKlub.Include(e => e.Klub).FirstOrDefault(e => e.ID == entity.ID);

            return mapper.Map<Entity.StatistikaKlub, Model.StatistikaKlubResponse>(responseEntity);
        }

        public List<Model.StatistikaKlubSezonaResponse> getSezonaStatistikaByKlub(int id)
        {
            var ligaId = context.Klub.Find(id).LigaID;
            var sezone = context.Sezona.Where(e => e.LigaID == ligaId).OrderByDescending(e => e.DatumPocetka).ToList();

            List<Model.StatistikaKlubSezonaResponse> response = new List<Model.StatistikaKlubSezonaResponse>();

            foreach (var sezona in sezone)
            {
                Model.StatistikaKlubSezonaResponse statistika = new Model.StatistikaKlubSezonaResponse();
                statistika.Sezona = $"{sezona.DatumPocetka.Year}/{sezona.DatumZavrsetka.Year}";
                var utakmice = context.Utakmica
                    .Where(e => e.DatumOdrzavanja >= sezona.DatumPocetka && e.DatumOdrzavanja < sezona.DatumZavrsetka && 
                    (e.KlubDomacinID == id || e.KlubGostID==id) &&
                    e.IsZavrsena)                     
                    .ToList();

                statistika.OdigraneUtakmice = utakmice.Count();
                statistika.Pobjeda = utakmice.Count(e =>
                    (e.KlubDomacinID == id && e.RezultatDomacin > e.RezultatGost) ||
                    (e.KlubGostID == id && e.RezultatDomacin < e.RezultatGost));
                statistika.Poraz = utakmice.Count(e =>
                    (e.KlubDomacinID == id && e.RezultatDomacin < e.RezultatGost) ||
                    (e.KlubGostID == id && e.RezultatDomacin > e.RezultatGost));
                statistika.Remi = utakmice.Count(e => e.RezultatDomacin == e.RezultatGost);

                statistika.PrimljeniGolovi = utakmice.Sum(e => e.KlubDomacinID == id ? e.RezultatGost : e.RezultatDomacin);
                statistika.PostignutiGolovi = utakmice.Sum(e => e.KlubDomacinID == id ? e.RezultatDomacin : e.RezultatGost); 
                
                response.Add(statistika);
            }

            return response;
        }
    }
}