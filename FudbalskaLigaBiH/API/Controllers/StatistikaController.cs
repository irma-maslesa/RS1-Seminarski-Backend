using Microsoft.AspNetCore.Mvc;
using API.Services;
using Model = Data.Model;
using Data;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "statistika-api")]
    public class StatistikaController
    {
        protected readonly IStatistikaService service;
        protected readonly ApplicationDbContext context;
        protected readonly IMapper mapper;

        public StatistikaController(IStatistikaService service, ApplicationDbContext context, IMapper mapper)
        {
            this.service = service;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public virtual Model.StatistikaResponse getByUtakmicaId(int id)
        {
            return service.getByUtakmicaId(id);
        }

        [HttpPost]
        public virtual Model.StatistikaResponse insert([FromBody] Model.StatistikaInsertRequest trener)
        {
            return service.insert(trener);
        }
    }
}
