using API.Services;
using Data;
using FudbalskaLigaBiH.API.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Certificate;
using System;
using System.Linq;
using Entity = Data.EntityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FudbalskaLigaBiH.API.JwtFeatures;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
                c.SwaggerDoc("statistika-api", new OpenApiInfo { Title = "statistika-api" });
                c.SwaggerDoc("igrac-api", new OpenApiInfo { Title = "igrac-api" });
                c.SwaggerDoc("stadion-api", new OpenApiInfo { Title = "stadion-api" });
                c.SwaggerDoc("trener-api", new OpenApiInfo { Title = "trener-api" });
                c.SwaggerDoc("klub-api", new OpenApiInfo { Title = "klub-api" });
                c.SwaggerDoc("utakmica-api", new OpenApiInfo { Title = "utakmica-api" });
                c.SwaggerDoc("account-api", new OpenApiInfo { Title = "account-api" });
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
            services.AddScoped<IUtakmicaService, UtakmicaService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IStatistikaIgracService, StatistikaIgracService>();
            services.AddScoped<IStatistikaKlubService, StatistikaKlubService>();
            services.AddScoped<IStatistikaService, StatistikaService>();
            services.AddScoped<IIgracService, IgracService>();

            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
            }));
            services.AddIdentity<Entity.Korisnik, Microsoft.AspNetCore.Identity.IdentityRole>(
                options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>();

            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                };
            });

            services.AddControllers(x =>
            {
                x.Filters.Add<ExceptionFilter>();
            });

            services.AddScoped<JwtHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "img"
                    )),
                RequestPath = "/Image"
            });

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/statistika-api/swagger.json", "Statistika API");
                c.SwaggerEndpoint("/swagger/igrac-api/swagger.json", "Igrac API");
                c.SwaggerEndpoint("/swagger/trener-api/swagger.json", "Trener API");
                c.SwaggerEndpoint("/swagger/stadion-api/swagger.json", "Stadion API");
                c.SwaggerEndpoint("/swagger/klub-api/swagger.json", "Klub API");
                c.SwaggerEndpoint("/swagger/utakmica-api/swagger.json", "Utakmica API");
                c.SwaggerEndpoint("/swagger/account-api/swagger.json", "Account API");
                c.RoutePrefix = "";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
