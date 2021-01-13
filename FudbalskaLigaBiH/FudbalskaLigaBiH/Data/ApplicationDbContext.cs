﻿using System;
using System.Collections.Generic;
using System.Text;
using FudbalskaLigaBiH.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FudbalskaLigaBiH.Data
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

            //modelBuilder.Entity<Klub>()
            //    .HasOne<Trener>(k => k.Trener)
            //    .WithOne(t => t.Klub)
            //    .HasForeignKey<Trener>(t => t.KlubID);

            //modelBuilder.Entity<Klub>()
            //    .HasOne<Stadion>(k => k.Stadion)
            //    .WithOne(s => s.Klub)
            //    .HasForeignKey<Stadion>(s => s.KlubID);

        }

        public DbSet<Novinar> Novinar { get; set; }
        public DbSet<Novost> Novost { get; set; }
        public DbSet<Pozicija> Pozicija { get; set; }
        public DbSet<Igrac> Igrac { get; set; }
    }
}
