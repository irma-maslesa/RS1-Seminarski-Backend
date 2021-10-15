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
    public class EntitetService : ReadService<Model.LoV, Entity.Entitet, object>, IEntitetService
    {
        public EntitetService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
