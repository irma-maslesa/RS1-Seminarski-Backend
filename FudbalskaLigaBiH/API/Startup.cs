using API.Services;
using Data;
using FudbalskaLigaBiH.API.Filter;
using FudbalskaLigaBiH.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("stadion-api", new OpenApiInfo { Title = "stadion-api"});
                c.SwaggerDoc("trener-api", new OpenApiInfo { Title = "trener-api" });
                c.SwaggerDoc("klub-api", new OpenApiInfo { Title = "klub-api" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
            });
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ITrenerService, TrenerService>();
            services.AddScoped<IEntitetService, EntitetService>(); 
            services.AddScoped<IGradService, GradService>();
            services.AddScoped<IStadionService, StadionService>();
            services.AddScoped<IKlubService, KlubService>();
            services.AddScoped<ILigaService, LigaService>();
            services.AddScoped<ISezonaService, SezonaService>();

            services.AddControllers(x => {
                x.Filters.Add<ExceptionFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/trener-api/swagger.json", "Trener API");
                c.SwaggerEndpoint("/swagger/stadion-api/swagger.json", "Stadion API");
                c.SwaggerEndpoint("/swagger/klub-api/swagger.json", "Klub API");
                c.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
