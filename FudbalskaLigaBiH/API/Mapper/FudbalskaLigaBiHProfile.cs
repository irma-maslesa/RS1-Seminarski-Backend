using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity = Data.EntityModel;
using Model = Data.Model;

namespace FudbalskaLigaBiH.API.Mapper
{
    public class FudbalskaLigaBiHProfile:Profile
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
                            opts=> opts.MapFrom(src => src.Trener.ID))
                .ForMember(dest => dest.TrenerImePrezime,
                            opts => opts.MapFrom(src => src.Trener.Ime + ' ' + src.Trener.Prezime))
                .ForMember(dest => dest.StadionID,
                            opts => opts.MapFrom(src => src.Stadion.ID))
                .ForMember(dest => dest.StadionNaziv,
                            opts => opts.MapFrom(src => src.Stadion.Naziv));
            CreateMap<Model.KlubUpsertRequest, Entity.Klub>();

            CreateMap<Entity.Liga, Model.LigaResponse>();

            CreateMap<Entity.Sezona, Model.SezonaResponse>();
            CreateMap<Model.SezonaUpsertRequest, Entity.Sezona>();
        }
    }
}
