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
    public class UtakmicaService :
        ReadService<Model.UtakmicaResponse, Entity.Utakmica, Model.UtakmicaSearchRequest>,
        IUtakmicaService
    {
        protected readonly ISezonaService sezonaService;

        public UtakmicaService(ApplicationDbContext context, IMapper mapper, ISezonaService sezonaService) : base(context, mapper)
        {
            this.sezonaService = sezonaService;
        }

        public override IList<Model.UtakmicaResponse> get(Model.UtakmicaSearchRequest search)
        {
            IQueryable<Entity.Utakmica> entitySet = context.Utakmica.AsQueryable();

            if (search?.Status != null)
            {
                if (search.Status.Equals("ZAVRSENA"))
                {
                    entitySet = entitySet.Where(e => e.IsZavrsena);
                }
                else if (search.Status.Equals("U TOKU"))
                {
                    entitySet = entitySet.Where(e => !e.IsZavrsena && e.MinutaIgre != -1);
                }
                else if (search.Status.Equals("PREDSTOJECA"))
                {
                    entitySet = entitySet.Where(e => e.MinutaIgre == -1 && !e.IsZavrsena);
                }
            }
            if (search?.Analitika != null)
            {
                var utakmiceIds = context.StatistikaKlub.Select(e => e.UtakmicaID).ToList();

                if (search.Analitika.Value)
                {
                    entitySet = entitySet.Where(e => utakmiceIds.Contains(e.UtakmicaID));
                }
                else
                {

                    entitySet = entitySet.Where(e => !utakmiceIds.Contains(e.UtakmicaID));
                }
            }
            if (search?.StadionId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.Stadion.ID == search.StadionId);
            }


            if (search?.KlubId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.ID == search.KlubId || e.KlubGost.ID == search.KlubId);
            }
            if (search?.KlubDomacinId != null)
            {
                entitySet = entitySet.Where(e => e.KlubDomacin.ID == search.KlubDomacinId);
            }
            if (search?.KlubGostId != null)
            {
                entitySet = entitySet.Where(e => e.KlubGost.ID == search.KlubGostId);
            }
            if (search?.SezonaIds != null)
            {
                entitySet = entitySet.Where(e => search.SezonaIds.Contains(e.Sezona.ID));
            }

            List<Entity.Utakmica> entityList = entitySet.Include(e => e.Liga).Include(e => e.KlubDomacin).Include(e => e.KlubGost).OrderByDescending(e => e.DatumOdrzavanja).ToList();

            return mapper.Map<List<Model.UtakmicaResponse>>(entityList);
        }

        public override Model.UtakmicaResponse getById(int id)
        {
            Entity.Utakmica entity = context.Utakmica.Include(e => e.Liga).Include(e => e.KlubDomacin).Include(e => e.KlubGost).Where(e => e.UtakmicaID == id).FirstOrDefault();

            if (entity == null)
                throw new UserException($"Utakmica ({id}) ne postoji!");

            return mapper.Map<Model.UtakmicaResponse>(entity);
        }
        public IList<Model.UtakmicaSimpleResponse> getLast5ByKlub(int klubId, int ligaId)
        {
            var trenutnaSezona = sezonaService.getTrenutnuSezonu(ligaId);

            List<Entity.Utakmica> entityList = new List<Entity.Utakmica>();
            if (trenutnaSezona != null)
            {
                entityList = context.Utakmica
                    .Include(e => e.KlubDomacin)
                    .Include(e => e.KlubGost)
                    .Where(e => e.DatumOdrzavanja > trenutnaSezona.DatumPocetka && e.DatumOdrzavanja < trenutnaSezona.DatumZavrsetka)
                    .Where(e => e.KlubDomacin.ID == klubId || e.KlubGost.ID == klubId)
                    .Where(e => e.IsZavrsena)
                    .OrderByDescending(e => e.DatumOdrzavanja)
                    .Take(5).ToList();
            }

            return mapper.Map<List<Model.UtakmicaSimpleResponse>>(entityList);
        }

        public Model.UtakmicaSimpleResponse getNextByKlub(int klubId, int ligaId)
        {
            var trenutnaSezona = sezonaService.getTrenutnuSezonu(ligaId);

            Entity.Utakmica entity = null;
            if (trenutnaSezona != null)
                entity = context.Utakmica
                .Include(e => e.KlubDomacin)
                .Include(e => e.KlubGost)
                .Where(e => e.DatumOdrzavanja > trenutnaSezona.DatumPocetka && e.DatumOdrzavanja < trenutnaSezona.DatumZavrsetka)
                .Where(e => e.KlubDomacin.ID == klubId || e.KlubGost.ID == klubId)
                .Where(e => !e.IsZavrsena)
                .OrderBy(e => e.DatumOdrzavanja)
                .Take(1).FirstOrDefault();

            return mapper.Map<Model.UtakmicaSimpleResponse>(entity);
        }

        public IList<Model.UtakmicaSimpleResponse> getByIgrac(int igracId, PaginationFilter filter)
        {
            var klubId = context.Igrac.Find(igracId).KlubID;
            var ligaId = context.Igrac.Include(e => e.Klub).Where(e => e.IgracID == igracId).FirstOrDefault()?.Klub.LigaID;
            var trenutnaSezona = sezonaService.getTrenutnuSezonu(ligaId.HasValue ? ligaId.Value : 0);

            List<Entity.Utakmica> klubUtakmice = new List<Entity.Utakmica>();
            if (trenutnaSezona != null)
                klubUtakmice = context.Utakmica
                    .Include(e => e.KlubDomacin)
                    .Include(e => e.KlubGost)
                    .Where(e => e.DatumOdrzavanja > trenutnaSezona.DatumPocetka && e.DatumOdrzavanja < trenutnaSezona.DatumZavrsetka)
                    .Where(e => e.KlubDomacin.ID == klubId || e.KlubGost.ID == klubId)
                    .Where(e => e.IsZavrsena)
                    .OrderByDescending(e => e.DatumOdrzavanja)
                    .ToList();

            List<int> igracUtakmiceIds = context.StatistikaIgrac.Where(e => e.IgracId == igracId).Select(e => e.UtakmicaID).ToList();

            List<Entity.Utakmica> igracUtakmice = klubUtakmice
                .Where(e => igracUtakmiceIds.Contains(e.UtakmicaID))
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return mapper.Map<List<Model.UtakmicaSimpleResponse>>(igracUtakmice);
        }

        public IList<Model.UtakmicaSimpleResponse> getByKlub(int klubId)
        {
            var ligaId = context.Klub.Where(e => e.ID == klubId).FirstOrDefault()?.LigaID;
            var trenutnaSezona = sezonaService.getTrenutnuSezonu(ligaId.HasValue ? ligaId.Value : 0);

            List<Entity.Utakmica> klubUtakmice = new List<Entity.Utakmica>();
            if (trenutnaSezona != null)
                klubUtakmice = context.Utakmica
                    .Include(e => e.KlubDomacin)
                    .Include(e => e.KlubGost)
                    .Where(e => e.KlubDomacin.ID == klubId || e.KlubGost.ID == klubId)
                    .Where(e => e.IsZavrsena)
                    .ToList();

            return mapper.Map<List<Model.UtakmicaSimpleResponse>>(klubUtakmice);
        }
    }
}
