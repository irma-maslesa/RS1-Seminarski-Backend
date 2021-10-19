using AutoMapper;
using System;
using Entity = Data.EntityModel;
using Model = Data.Model;

namespace FudbalskaLigaBiH.API.Mapper
{
    public class FudbalskaLigaBiHProfile : Profile
    {
        public FudbalskaLigaBiHProfile()
        {
            CreateMap<Entity.Trener, Model.TrenerResponse>();
            CreateMap<Model.TrenerUpsertRequest, Entity.Trener>();

            CreateMap<Entity.Entitet, Model.LoV>();

            CreateMap<Entity.Grad, Model.GradResponse>();
            CreateMap<Model.GradUpsertRequest, Entity.Grad>();

            CreateMap<Entity.Stadion, Model.StadionResponse>();
            CreateMap<Model.StadionUpsertRequest, Entity.Stadion>();

            CreateMap<Entity.Klub, Model.KlubResponse>()
                .ForMember(dest => dest.TrenerID,
                            opts => opts.MapFrom(src => src.Trener.ID))
                .ForMember(dest => dest.TrenerImePrezime,
                            opts => opts.MapFrom(src => src.Trener.Ime + ' ' + src.Trener.Prezime))
                .ForMember(dest => dest.StadionID,
                            opts => opts.MapFrom(src => src.Stadion.ID))
                .ForMember(dest => dest.StadionNaziv,
                            opts => opts.MapFrom(src => src.Stadion.Naziv))
                .ForMember(dest => dest.Slika,
                            opts => opts.MapFrom(src => src.Slika != null ? $"/Image/{src.Slika}" : "/Image/no-image.jpg"));
            CreateMap<Entity.Klub, Model.KlubPoredakResponse>()
                .ForMember(dest => dest.Slika,
                            opts => opts.MapFrom(src => src.Slika != null ? $"/Image/{src.Slika}" : "/Image/no-image.jpg"));
            CreateMap<Entity.Klub, Model.LoV>();
            CreateMap<Model.KlubUpsertRequest, Entity.Klub>()
                .ForMember(dest => dest.Slika,
                            opts => opts.Ignore());

            CreateMap<Entity.Liga, Model.LigaResponse>();
            CreateMap<Model.LigaUpsertRequest, Entity.Liga>();

            CreateMap<Entity.Sezona, Model.SezonaResponse>();
            CreateMap<Model.SezonaUpsertRequest, Entity.Sezona>();

            CreateMap<Entity.Utakmica, Model.UtakmicaResponse>()
                .ForMember(dest => dest.ID,
                            opts => opts.MapFrom(src => src.UtakmicaID))
                .ForMember(dest => dest.KlubDomacinID,
                            opts => opts.MapFrom(src => src.KlubDomacin.ID))
                .ForMember(dest => dest.KlubDomacinNaziv,
                            opts => opts.MapFrom(src => src.KlubDomacin.Naziv))
                .ForMember(dest => dest.KlubDomacinSlika,
                            opts => opts.MapFrom(src => src.KlubDomacin.Slika != null ? $"/Image/{src.KlubDomacin.Slika}" : "/Image/no-image.jpg"))
                .ForMember(dest => dest.KlubGostID,
                            opts => opts.MapFrom(src => src.KlubGost.ID))
                .ForMember(dest => dest.KlubGostNaziv,
                            opts => opts.MapFrom(src => src.KlubGost.Naziv))
                .ForMember(dest => dest.KlubGostSlika,
                            opts => opts.MapFrom(src => src.KlubGost.Slika != null ? $"/Image/{src.KlubGost.Slika}" : "/Image/no-image.jpg"));
            CreateMap<Entity.Utakmica, Model.UtakmicaSimpleResponse>()
                .ForMember(dest => dest.ID,
                            opts => opts.MapFrom(src => src.UtakmicaID))
                .ForMember(dest => dest.KlubDomacinID,
                            opts => opts.MapFrom(src => src.KlubDomacin.ID))
                .ForMember(dest => dest.KlubDomacinNaziv,
                            opts => opts.MapFrom(src => src.KlubDomacin.Naziv))
                .ForMember(dest => dest.KlubGostID,
                            opts => opts.MapFrom(src => src.KlubGost.ID))
                .ForMember(dest => dest.KlubGostNaziv,
                            opts => opts.MapFrom(src => src.KlubGost.Naziv));

            CreateMap<Entity.Korisnik, Model.KorisnikResponse>();
            CreateMap<Entity.KorisnikUtakmica, Model.OmiljenaUtakmicaRequest>().ReverseMap();

            CreateMap<Entity.StatistikaIgrac, Model.StatistikaIgracResponse>()
                .ForMember(dest => dest.IgracImePrezime,
                            opts => opts.MapFrom(src => $"{src.Igrac.Ime} {src.Igrac.Prezime}"))
                .ForMember(dest => dest.IgracId,
                            opts => opts.MapFrom(src => src.Igrac.IgracID))
                .ForMember(dest => dest.IgracBrojDresa,
                            opts => opts.MapFrom(src => src.Igrac.BrojDresa));
            CreateMap<Model.StatistikaIgracInsertRequest, Entity.StatistikaIgrac>();

            CreateMap<Entity.StatistikaKlub, Model.StatistikaKlubResponse>()
               .ForMember(dest => dest.KlubNaziv,
                           opts => opts.MapFrom(src => src.Klub.Naziv))
               .ForMember(dest => dest.KlubId,
                           opts => opts.MapFrom(src => src.Klub.ID))
               .ForMember(dest => dest.KlubSlika,
                           opts => opts.MapFrom(src => src.Klub.Slika != null ? $"/Image/{src.Klub.Slika}" : "/Image/no-image.jpg"));
            CreateMap<Model.StatistikaKlubInsertRequest, Entity.StatistikaKlub>();

            CreateMap<Entity.Igrac, Model.IgracSimpleResponse>()
               .ForMember(dest => dest.ID,
                           opts => opts.MapFrom(src => src.IgracID))
               .ForMember(dest => dest.ImePrezime,
                           opts => opts.MapFrom(src => $"{src.Ime} {src.Prezime}"))
               .ForMember(dest => dest.Klub,
                           opts => opts.MapFrom(src => src.Klub.Naziv))
              .ForMember(dest => dest.Pozicija,
                          opts => opts.MapFrom(src => src.Pozicija.NazivPozicije));

            CreateMap<Entity.Igrac, Model.IgracResponse>()
              .ForMember(dest => dest.ID,
                          opts => opts.MapFrom(src => src.IgracID))
              .ForMember(dest => dest.ImePrezime,
                          opts => opts.MapFrom(src => $"{src.Ime} {src.Prezime}"))
              .ForMember(dest => dest.Pozicija,
                          opts => opts.MapFrom(src => src.Pozicija.NazivPozicije))
              .ForMember(dest => dest.Godine,
                          opts => opts.MapFrom(src => DateTime.Now.Subtract(src.DatumRodjenja).Days / 365));
        }
    }
}
