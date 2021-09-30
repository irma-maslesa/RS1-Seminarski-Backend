using AutoMapper;
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

            CreateMap<Entity.Entitet, Model.EntitetResponse>();

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
                            opts => opts.MapFrom(src => src.Slika != null ? $"/Image/{src.Slika}" : null));
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
                            opts => opts.MapFrom(src => src.KlubDomacin.Slika != null ? $"/Image/{src.KlubDomacin.Slika}" : null))
                .ForMember(dest => dest.KlubGostID,
                            opts => opts.MapFrom(src => src.KlubGost.ID))
                .ForMember(dest => dest.KlubGostNaziv,
                            opts => opts.MapFrom(src => src.KlubGost.Naziv))
                .ForMember(dest => dest.KlubGostSlika,
                            opts => opts.MapFrom(src => src.KlubGost.Slika != null ? $"/Image/{src.KlubGost.Slika}" : null));

            CreateMap<Entity.Korisnik, Model.KorisnikResponse>();
            CreateMap<Entity.KorisnikUtakmica, Model.OmiljenaUtakmicaRequest>().ReverseMap();
        }
    }
}
