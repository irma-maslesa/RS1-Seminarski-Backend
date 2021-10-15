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
    public class KlubService :
        CRUDService<Model.KlubResponse, Entity.Klub, Model.KlubSearchRequest, Model.KlubUpsertRequest, Model.KlubUpsertRequest>,
        IKlubService
    {
        protected readonly IUtakmicaService utakmicaService;
        protected readonly ISezonaService sezonaService;

        public KlubService(ApplicationDbContext context, IMapper mapper, IUtakmicaService utakmicaService, ISezonaService sezonaService) : base(context, mapper)
        {
            this.utakmicaService = utakmicaService;
            this.sezonaService = sezonaService;
        }

        public override IList<Model.KlubResponse> get(Model.KlubSearchRequest search)
        {
            IQueryable<Entity.Klub> entitySet = context.Klub.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entitySet = entitySet.Where(e => e.Naziv.ToLower().Contains(search.Naziv.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Mail))
            {
                entitySet = entitySet.Where(e => e.Mail.ToLower().Contains(search.Mail.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(search?.Adresa))
            {
                entitySet = entitySet.Where(e => e.Adresa.ToLower().Contains(search.Adresa.ToLower()));
            }
            if (search?.TrenerId != null)
            {
                entitySet = entitySet.Where(e => e.Trener.ID == search.TrenerId);
            }
            if (search?.StadionId != null)
            {
                entitySet = entitySet.Where(e => e.Stadion.ID == search.StadionId);
            }
            if (search?.LigaId != null)
            {
                entitySet = entitySet.Where(e => e.Liga.ID == search.LigaId);
            }

            List<Entity.Klub> entityList = entitySet.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).ToList();

            return mapper.Map<List<Model.KlubResponse>>(entityList);
        }

        public override Model.KlubResponse getById(int id)
        {
            Entity.Klub entity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Klub({id}) ne postoji!");


            return mapper.Map<Model.KlubResponse>(entity);
        }

        public override Model.KlubResponse insert(Model.KlubUpsertRequest request)
        {
            string errorMessage = string.Empty;
            if (request.IgraciIds != null)
            {
                foreach (int igracId in request.IgraciIds)
                {
                    Entity.Igrac igracEntity = context.Igrac.Find(igracId);
                    if (igracEntity == null)
                        errorMessage += $"Igrač({igracId}) ne postoji! ";
                    else if (igracEntity.KlubID != null)
                        errorMessage += $"Igrač({igracId}) je već dodijeljen klubu! ";
                }
            }

            if (request.TrenerId != null)
            {
                Entity.Trener trenerEntity = context.Trener.Find(request.TrenerId);
                if (trenerEntity == null)
                    errorMessage += $"Trener({request.TrenerId}) ne postoji! ";
                else
                {
                    Entity.Klub trenerKlub = context.Klub.Where(e => e.TrenerID == request.TrenerId).FirstOrDefault();
                    if (trenerKlub != null)
                        errorMessage += $"Trener({request.TrenerId}) je već dodijeljen klubu! ";
                }
            }

            Entity.Stadion stadionEntity = context.Stadion.Find(request.StadionId);
            if (stadionEntity == null)
                errorMessage += $"Stadion({request.StadionId}) ne postoji! ";
            else if (stadionEntity.Klub != null)
                errorMessage += $"Stadion({request.StadionId}) je već dodijeljen klubu! ";

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Klub entity = mapper.Map<Model.KlubUpsertRequest, Entity.Klub>(request);

            if (request.Slika != null)
            {
                string ekstenzija = System.IO.Path.GetExtension(request.Slika.FileName);
                string contentType = request.Slika.ContentType;

                var fileName = $"{Guid.NewGuid()}{ekstenzija}";
                string folder = "img/";
                bool exist = Directory.Exists(folder);
                if (!exist)
                    Directory.CreateDirectory(folder);

                request.Slika.CopyTo(new System.IO.FileStream(folder + fileName, FileMode.Create));
                entity.Slika = fileName;
            }

            entity = context.Klub.Add(entity).Entity;

            context.SaveChanges();

            Entity.Klub responseEntity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == entity.ID);


            return mapper.Map<Entity.Klub, Model.KlubResponse>(responseEntity);
        }

        public override Model.KlubResponse update(int id, Model.KlubUpsertRequest request)
        {
            string errorMessage = string.Empty;

            if (request.IgraciIds != null)
            {
                foreach (int igracId in request.IgraciIds)
                {
                    Entity.Igrac igracEntity = context.Igrac.Find(igracId);
                    if (igracEntity == null)
                        errorMessage += $"Igrac({igracId}) ne postoji! ";
                    else if (igracEntity.Klub != null && igracEntity.Klub.ID != id)
                        errorMessage += $"Igrač({igracId}) je već dodijeljen klubu! ";
                }
            }

            if (request.TrenerId != null)
            {
                Entity.Trener trenerEntity = context.Trener.Find(request.TrenerId);
                if (trenerEntity == null)
                    errorMessage += $"Trener({request.TrenerId}) ne postoji! ";
                else
                {
                    Entity.Klub trenerKlub = context.Klub.Where(e => e.TrenerID == request.TrenerId).FirstOrDefault();
                    if (trenerKlub != null && trenerKlub.ID != id)
                        errorMessage += $"Trener({request.TrenerId}) je već dodijeljen klubu! ";
                }
            }

            Entity.Stadion stadionEntity = context.Stadion.Find(request.StadionId);
            if (stadionEntity == null)
                errorMessage += $"Stadion({request.StadionId}) ne postoji! ";
            else if (stadionEntity.Klub != null && stadionEntity.Klub.ID != id)
                errorMessage += $"Stadion({request.StadionId}) je već dodijeljen klubu! ";

            Entity.Liga ligaEntity = context.Liga.Find(request.LigaId);
            if (ligaEntity == null)
                errorMessage += $"Liga({request.LigaId}) ne postoji!";

            if (errorMessage != string.Empty)
                throw new UserException(errorMessage);

            Entity.Klub entity = context.Klub.FirstOrDefault(e => e.ID == id);

            if (entity == null)
                throw new UserException($"Klub({id}) ne postoji! ");

            entity = mapper.Map(request, entity);

            if (request.Slika != null)
            {
                string ekstenzija = System.IO.Path.GetExtension(request.Slika.FileName);
                string contentType = request.Slika.ContentType;

                var fileName = $"{Guid.NewGuid()}{ekstenzija}";
                string folder = "img/";
                bool exist = Directory.Exists(folder);
                if (!exist)
                    Directory.CreateDirectory(folder);

                request.Slika.CopyTo(new System.IO.FileStream(folder + fileName, FileMode.Create));
                entity.Slika = fileName;
            }

            context.Klub.Update(entity);
            context.SaveChanges();

            Entity.Klub responseEntity = context.Klub.Include(e => e.Trener).Include(e => e.Stadion).Include(e => e.Liga).FirstOrDefault(e => e.ID == id);


            return mapper.Map<Entity.Klub, Model.KlubResponse>(responseEntity);
        }


        public IList<Model.KlubPoredakResponse> getPoredak(int klubId = 0, int liga = 0)
        {
            var ligaId = klubId != 0 ? context.Klub.Find(klubId).LigaID : liga;
            var trenutnaSezona = sezonaService.getTrenutnuSezonu(ligaId);
            var entityList = context.Klub.Where(e => e.LigaID == ligaId).ToList();

            var response = mapper.Map<List<Model.KlubPoredakResponse>>(entityList);

            foreach (var klub in response)
            {
                List<Entity.Utakmica> utakmice;

                if (trenutnaSezona != null)
                    utakmice = context.Utakmica.Where(e => e.KlubDomacin.ID == klub.ID || e.KlubGost.ID == klub.ID)
                        .Where(e => e.DatumOdrzavanja > trenutnaSezona.DatumPocetka && e.DatumOdrzavanja < trenutnaSezona.DatumZavrsetka)
                        .Where(e => e.IsZavrsena).ToList();
                else
                    return new List<Model.KlubPoredakResponse>();

                klub.OdigraneUtakmice = utakmice.Count;
                klub.Pobjede = utakmice.Where(e => e.KlubDomacinID == klub.ID ? e.RezultatDomacin > e.RezultatGost : e.RezultatDomacin < e.RezultatGost).ToList().Count;
                klub.Porazi = utakmice.Where(e => e.KlubDomacinID == klub.ID ? e.RezultatDomacin < e.RezultatGost : e.RezultatDomacin > e.RezultatGost).ToList().Count;
                klub.Remi = utakmice.Where(e => e.RezultatDomacin == e.RezultatGost).ToList().Count;
                klub.PostignutiGolovi = utakmice.Sum(e => e.KlubDomacinID == klub.ID ? e.RezultatDomacin : e.RezultatGost);
                klub.PrimljeniGolovi = utakmice.Sum(e => e.KlubDomacinID != klub.ID ? e.RezultatDomacin : e.RezultatGost);
                klub.Bodovi = klub.Pobjede * 3 + klub.Remi;


                klub.prethodneUtakmice = utakmicaService.getLast5ByKlub(klub.ID, ligaId).ToList();
                klub.iducaUtakmica = utakmicaService.getNextByKlub(klub.ID, ligaId);
            }

            return response.OrderByDescending(e => e.Bodovi).ThenByDescending(e => e.PostignutiGolovi - e.PrimljeniGolovi).ToList();
        }

        public IList<Model.LoV> getLoVs()
        {
            List<Entity.Klub> entityList = context.Klub.ToList();

            return mapper.Map<List<Model.LoV>>(entityList);
        }
    }
}