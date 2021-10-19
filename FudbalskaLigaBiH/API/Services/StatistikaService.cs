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
    public class StatistikaService:
        IStatistikaService
    {
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public StatistikaService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Model.StatistikaResponse getByUtakmicaId(int id)
        {
            Entity.Utakmica utakmicaEntity = context.Utakmica.Include(e => e.KlubDomacin).Include(e => e.KlubGost).FirstOrDefault(e => e.UtakmicaID == id);

            if (utakmicaEntity == null)
                throw new UserException($"Utakmica ({id}) ne postoji!");

            Model.StatistikaResponse statistika = new Model.StatistikaResponse();
            statistika.UtakmicaID = id;
            statistika.RezultatDomacin = utakmicaEntity.RezultatDomacin;
            statistika.RezultatGost = utakmicaEntity.RezultatGost;

            Entity.StatistikaKlub entityStatistikaDomacin = context.StatistikaKlub.Include(e => e.Klub).FirstOrDefault(e => e.Klub.ID == utakmicaEntity.KlubDomacin.ID && e.Utakmica.UtakmicaID == id);
            Entity.StatistikaKlub entityStatistikaGost = context.StatistikaKlub.Include(e => e.Klub).FirstOrDefault(e => e.Klub.ID == utakmicaEntity.KlubGost.ID && e.Utakmica.UtakmicaID == id);
            statistika.statistikaDomacin = mapper.Map<Model.StatistikaKlubResponse>(entityStatistikaDomacin);
            statistika.statistikaGost = mapper.Map<Model.StatistikaKlubResponse>(entityStatistikaGost);

            List<Entity.StatistikaIgrac> entityStatistikaDomaciIgraci = context.StatistikaIgrac.Include(e => e.Igrac).Where(e => e.Igrac.Klub.ID == utakmicaEntity.KlubDomacin.ID && e.Utakmica.UtakmicaID == id).ToList();
            List<Entity.StatistikaIgrac> entityStatistikaGostujuciIgraci = context.StatistikaIgrac.Include(e => e.Igrac).Where(e => e.Igrac.Klub.ID == utakmicaEntity.KlubGost.ID && e.Utakmica.UtakmicaID == id).ToList();
            statistika.statistikaDomaciIgraci = mapper.Map< List<Model.StatistikaIgracResponse>>(entityStatistikaDomaciIgraci);
            statistika.statistikaGostujuciIgraci = mapper.Map< List<Model.StatistikaIgracResponse>>(entityStatistikaGostujuciIgraci);

            return statistika;
        }

        public  Model.StatistikaResponse insert(Model.StatistikaInsertRequest request)
        {
            Entity.Utakmica utakmicaEntity = context.Utakmica.Find(request.UtakmicaID);

            if (utakmicaEntity == null)
                throw new UserException($"Utakmica ({request.UtakmicaID}) ne postoji!");

            utakmicaEntity.RezultatDomacin = request.RezultatDomacin;
            utakmicaEntity.RezultatGost = request.RezultatGost;
            context.Update(utakmicaEntity);

            Entity.StatistikaKlub entityStatistikaDomacin = mapper.Map<Entity.StatistikaKlub>(request.statistikaDomacin);
            entityStatistikaDomacin.UtakmicaID = request.UtakmicaID;
            entityStatistikaDomacin.ZutiKarton = request.statistikaDomaciIgraci.Sum(e => e.ZutiKarton);
            context.Add(entityStatistikaDomacin);

            Entity.StatistikaKlub entityStatistikaGost = mapper.Map<Entity.StatistikaKlub>(request.statistikaGost);
            entityStatistikaGost.UtakmicaID = request.UtakmicaID;
            entityStatistikaGost.ZutiKarton = request.statistikaGostujuciIgraci.Sum(e => e.ZutiKarton);
            context.Add(entityStatistikaGost);

            List<Entity.StatistikaIgrac> entityStatistikaDomaciIgraci = new List<StatistikaIgrac>();
            foreach (var statIgrac in request.statistikaDomaciIgraci)
            {
                Entity.StatistikaIgrac entityStatistikaIgrac = mapper.Map<Entity.StatistikaIgrac>(statIgrac);
                entityStatistikaIgrac.UtakmicaID = request.UtakmicaID;

                entityStatistikaDomaciIgraci.Add(entityStatistikaIgrac);
            }
            context.AddRange(entityStatistikaDomaciIgraci);

            List<Entity.StatistikaIgrac> entityStatistikaGostujuciIgraci = new List<StatistikaIgrac>();
            foreach (var statIgrac in request.statistikaGostujuciIgraci)
            {
                Entity.StatistikaIgrac entityStatistikaIgrac = mapper.Map<Entity.StatistikaIgrac>(statIgrac);
                entityStatistikaIgrac.UtakmicaID = request.UtakmicaID;

                entityStatistikaGostujuciIgraci.Add(entityStatistikaIgrac);
            }
            context.AddRange(entityStatistikaGostujuciIgraci);

            context.SaveChanges();

            Model.StatistikaResponse statistika = new Model.StatistikaResponse();
            statistika.RezultatDomacin = utakmicaEntity.RezultatDomacin;
            statistika.RezultatGost = utakmicaEntity.RezultatGost;

            statistika.statistikaDomacin = mapper.Map<Model.StatistikaKlubResponse>(entityStatistikaDomacin);
            statistika.statistikaGost = mapper.Map<Model.StatistikaKlubResponse>(entityStatistikaGost);

            statistika.statistikaDomaciIgraci = mapper.Map<List<Model.StatistikaIgracResponse>>(entityStatistikaDomaciIgraci);
            statistika.statistikaGostujuciIgraci = mapper.Map<List<Model.StatistikaIgracResponse>>(entityStatistikaGostujuciIgraci);

            return statistika;
        }
    }
}