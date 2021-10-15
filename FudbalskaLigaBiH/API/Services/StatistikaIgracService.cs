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
    public class StatistikaIgracService :
        CRUDService<Model.StatistikaIgracResponse, Entity.StatistikaIgrac, object, Model.StatistikaIgracInsertRequest, object>,
        IStatistikaIgracService
    {
        protected readonly IUtakmicaService utakmicaService;
        public StatistikaIgracService(ApplicationDbContext context, IMapper mapper, IUtakmicaService utakmicaService) : base(context, mapper)
        {
            this.utakmicaService = utakmicaService;
        }

        public override IList<Model.StatistikaIgracResponse> get(object search)
        {
            List<Entity.StatistikaIgrac> entityList = context.StatistikaIgrac.Include(e => e.Igrac).ToList();

            return mapper.Map<List<Model.StatistikaIgracResponse>>(entityList);
        }

        public override Model.StatistikaIgracResponse getById(int id)
        {
            Entity.StatistikaIgrac entity = context.StatistikaIgrac.Include(e => e.Igrac).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Statistika Igrac({id}) ne postoji!");


            return mapper.Map<Model.StatistikaIgracResponse>(entity);
        }

        public override Model.StatistikaIgracResponse insert(Model.StatistikaIgracInsertRequest request)
        {
            string errorMessage = string.Empty;

            Entity.Igrac igracEntity = context.Igrac.Find(request.IgracId);
            if (igracEntity == null)
                errorMessage += $"Igrac({request.IgracId}) ne postoji!"; ;

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.StatistikaIgrac entity = mapper.Map<Entity.StatistikaIgrac>(request);

            entity = context.StatistikaIgrac.Add(entity).Entity;

            context.SaveChanges();

            Entity.StatistikaIgrac responseEntity = context.StatistikaIgrac.Include(e => e.Igrac).FirstOrDefault(e => e.ID == entity.ID);

            return mapper.Map<Model.StatistikaIgracResponse>(responseEntity);
        }

        public List<Model.StatistikaIgracSezonaResponse> getSezonaStatistikaByIgrac(int igracId)
        {
            var ligaId = context.Igrac.Include(e => e.Klub).Where(e => e.IgracID == igracId).FirstOrDefault()?.Klub.LigaID;
            var sezone = context.Sezona.Where(e => e.LigaID == ligaId).OrderByDescending(e => e.DatumPocetka).ToList();

            List<Model.StatistikaIgracSezonaResponse> response = new List<Model.StatistikaIgracSezonaResponse>();

            foreach (var sezona in sezone)
            {
                Model.StatistikaIgracSezonaResponse statistika = new Model.StatistikaIgracSezonaResponse();
                statistika.Sezona = $"{sezona.DatumPocetka.Year}/{sezona.DatumZavrsetka.Year}";
                var utakmiceIds = context.Utakmica
                    .Where(e => e.DatumOdrzavanja >= sezona.DatumPocetka && e.DatumOdrzavanja < sezona.DatumZavrsetka)
                    .Select(e => e.UtakmicaID)
                    .ToList();

                var statistike = context.StatistikaIgrac.Where(e => e.IgracId == igracId && utakmiceIds.Contains(e.UtakmicaID)).ToList();

                if (statistike.Count > 0)
                {
                    statistika.OdigraneUtakmice = statistike.Count();
                    statistika.BrojMinuta = Math.Round(statistike.Average(e => e.BrojMinuta * 1.0), 2);
                    statistika.Golovi = statistike.Sum(e => e.Golovi);
                    statistika.Asistencije = statistike.Sum(e => e.Asistencije);
                    statistika.ZutiKarton = statistike.Sum(e => e.ZutiKarton);
                    statistika.CrveniKarton = statistike.Sum(e => e.CrveniKarton);
                }
                else
                {
                    statistika.OdigraneUtakmice = 0;
                    statistika.BrojMinuta = 0;
                    statistika.Golovi = 0;
                    statistika.Asistencije = 0;
                    statistika.ZutiKarton = 0;
                    statistika.CrveniKarton = 0;
                }


                response.Add(statistika);
            }

            return response;
        }

        public List<Model.StatistikaIgracUtakmicaResponse> getUtakmicaStatistikaByIgrac(int igracId, PaginationFilter filter)
        {
            var utakmice = utakmicaService.getByIgrac(igracId, filter);
            List<Model.StatistikaIgracUtakmicaResponse> response = new List<Model.StatistikaIgracUtakmicaResponse>();

            foreach (var utakmica in utakmice)
            {
                Model.StatistikaIgracUtakmicaResponse statistika = new Model.StatistikaIgracUtakmicaResponse();
                statistika.utakmica = utakmica;

                var statistikaUtakmica = context.StatistikaIgrac.Where(e => e.IgracId == igracId && e.UtakmicaID == utakmica.ID).FirstOrDefault();

                statistika.BrojMinuta = statistikaUtakmica.BrojMinuta;
                statistika.Golovi = statistikaUtakmica.Golovi;
                statistika.Asistencije = statistikaUtakmica.Asistencije;
                statistika.ZutiKarton = statistikaUtakmica.ZutiKarton;
                statistika.CrveniKarton = statistikaUtakmica.CrveniKarton;


                response.Add(statistika);
            }

            return response;
        }
    }
}