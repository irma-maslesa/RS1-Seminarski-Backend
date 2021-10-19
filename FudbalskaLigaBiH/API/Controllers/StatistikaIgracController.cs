using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using API.Services;
using Model = Data.Model;
using Entity = Data.EntityModel;
using Data;
using AutoMapper;
using System.Linq;
using FudbalskaLigaBiH.API.Filter;

namespace API.Controllers
{
    [ApiController]
    [Route("statistika-igrac")]
    [ApiExplorerSettings(GroupName = "statistika-api")]
    public class StatistikaIgracController
    {
        protected readonly IStatistikaIgracService service;
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public StatistikaIgracController(IStatistikaIgracService service, ApplicationDbContext context, IMapper mapper)
        {
            this.service = service;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public virtual IList<Model.StatistikaIgracResponse> get([FromQuery] object search)
        {
            return service.get(search);
        }

        [HttpGet("{id}")]
        public virtual Model.StatistikaIgracResponse getById(int id)
        {
            return service.getById(id);
        }

        [HttpPost]
        public virtual Model.StatistikaIgracResponse insert([FromBody] Model.StatistikaIgracInsertRequest trener)
        {
            return service.insert(trener);
        }

        [HttpDelete("{id}")]
        public void delete(int id)
        {
            service.delete(id);
        }

        [HttpGet]
        [Route("{id}/sezone")]
        public List<Model.StatistikaIgracSezonaResponse> getSezonaStatistikaByIgrac(int id)
        {
            return service.getSezonaStatistikaByIgrac(id);
        }


        [HttpGet]
        [Route("{id}/utakmice")]
        public IList<Model.StatistikaIgracUtakmicaResponse> GetUtakmicaStatistikaByIgrac(int id, [FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var response = service.getUtakmicaStatistikaByIgrac(id, validFilter);
            return response;
        }

    }
}
