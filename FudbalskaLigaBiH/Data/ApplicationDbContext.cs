using System.Linq;
using Data.EntityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trener> Trener { get; set; }
        public DbSet<Entitet> Entitet { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Stadion> Stadion { get; set; }
        public DbSet<Sezona> Sezona { get; set; }
        public DbSet<Liga> Liga { get; set; }
        public DbSet<Klub> Klub { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //promjena kaskadnog brisanja
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<Klub>()
            //    .HasOne<Trener>(k => k.Trener)
            //    .WithOne(t => t.Klub)
            //    .HasForeignKey<Trener>(t => t.KlubID);

            //modelBuilder.Entity<Klub>()
            //    .HasOne<Stadion>(k => k.Stadion)
            //    .WithOne(s => s.Klub)
            //    .HasForeignKey<Stadion>(s => s.KlubID);


            /*
            modelBuilder.Entity<IdentityRole>()
                .HasData
                (
                    new IdentityRole { Id="1",Name="SuperAdmin",NormalizedName="SUPERADMIN"},
                    //new IdentityRole { Id = "2", Name = "GostKorisnik", NormalizedName = "GOSTKORISNIK" },
                    new IdentityRole { Id = "3", Name = "Novinar", NormalizedName = "NOVINAR" },
                    new IdentityRole { Id = "4", Name = "AdminIgraca", NormalizedName = "ADMINIGRACA" },
                    new IdentityRole { Id = "5", Name = "AdminUtakmica", NormalizedName = "ADMINUTAKMICA" },
                    new IdentityRole { Id = "6", Name = "AdminKlubova", NormalizedName = "ADMINKLUBOVA" },
                    new IdentityRole { Id = "7", Name = "Analiticar", NormalizedName = "ANALITICAR" }
                );

           
            PasswordHasher<Korisnik> ph = new PasswordHasher<Korisnik>();                  

            var SuperAdmin = new Korisnik()
            {
                Id = "1",
                Ime = "SuperAdmin",
                Prezime = "FIT",
                Email = "SuperAdmin@sport.com",
                NormalizedEmail = "SuperAdmin@sport.com".ToUpper(),
                UserName = "SuperAdmin",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            SuperAdmin.PasswordHash = ph.HashPassword(SuperAdmin, "SuperAdmin123.");

            var Novinar = new Korisnik()
            {
                Id = "2",
                Ime = "Novinar",
                Prezime = "FIT",
                Email = "Novinar@sport.com",
                NormalizedEmail = "Novinar@sport.com".ToUpper(),
                UserName = "Novinar",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            Novinar.PasswordHash = ph.HashPassword(Novinar, "Novinar123.");

            var AdminIgraca = new Korisnik()
            {
                Id = "3",
                Ime = "AdminIgraca",
                Prezime = "FIT",
                Email = "AdminIgraca@sport.com",
                NormalizedEmail = "AdminIgraca@sport.com".ToUpper(),
                UserName = "AdminIgraca",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            AdminIgraca.PasswordHash = ph.HashPassword(AdminIgraca, "AdminIgraca123.");

            var AdminUtakmica = new Korisnik()
            {
                Id = "3",
                Ime = "AdminUtakmica",
                Prezime = "FIT",
                Email = "AdminUtakmica@sport.com",
                NormalizedEmail = "AdminUtakmica@sport.com".ToUpper(),
                UserName = "AdminUtakmica",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            AdminUtakmica.PasswordHash = ph.HashPassword(AdminUtakmica, "AdminUtakmica123.");

            var AdminKlubova = new Korisnik()
            {
                Id = "4",
                Ime = "AdminKlubova",
                Prezime = "FIT",
                Email = "AdminKlubova@sport.com",
                NormalizedEmail = "AdminKlubova@sport.com".ToUpper(),
                UserName = "AdminKlubova",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            AdminKlubova.PasswordHash = ph.HashPassword(AdminKlubova, "AdminKlubova123.");

            var Analiticar = new Korisnik()
            {
                Id = "5",
                Ime = "Analiticar",
                Prezime = "FIT",
                Email = "Analiticar@sport.com",
                NormalizedEmail = "Analiticar@sport.com".ToUpper(),
                UserName = "Analiticar",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            Analiticar.PasswordHash = ph.HashPassword(Analiticar, "Analiticar123.");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData
                (
                    new IdentityUserRole<string>()
                    {
                        RoleId = "1",
                        UserId = "1"
                    }
                );
            */
        }

        public DbSet<Novost> Novost { get; set; }
        public DbSet<Pozicija> Pozicija { get; set; }
        public DbSet<Igrac> Igrac { get; set; }
        public DbSet<Utakmica> Utakmica { get; set; }
        public DbSet<KorisnikUtakmica> KorisnikUtakmica { get; set; }
        public DbSet<StatistikaIgrac> StatistikaIgrac { get; set; }
        public DbSet<StatistikaKlub> StatistikaKlub { get; set; }

    }
}
