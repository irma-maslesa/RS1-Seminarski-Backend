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
    [Route("statistika-klub")]
    [ApiExplorerSettings(GroupName = "statistika-api")]
    public class StatistikaKlubController
    {
        protected readonly IStatistikaKlubService service;
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public StatistikaKlubController(IStatistikaKlubService service, ApplicationDbContext context, IMapper mapper)
        {
            this.service = service;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public virtual IList<Model.StatistikaKlubResponse> get([FromQuery] object search)
        {
            return service.get(search);
        }

        [HttpGet("{id}")]
        public virtual Model.StatistikaKlubResponse getById(int id)
        {
            return service.getById(id);
        }

        [HttpPost]
        public virtual Model.StatistikaKlubResponse insert([FromBody] Model.StatistikaKlubInsertRequest statistika)
        {
            return service.insert(statistika);
        }

        [HttpDelete("{id}")]
        public void delete(int id)
        {
            service.delete(id);
        }

        [HttpGet]
        [Route("{id}/sezone")]
        public List<Model.StatistikaKlubSezonaResponse> getSezonaStatistikaByKlub(int id)
        {
            var response = service.getSezonaStatistikaByKlub(id);
            return response;
        }
    }
}
