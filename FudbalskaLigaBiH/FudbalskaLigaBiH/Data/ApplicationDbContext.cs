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

        public DbSet<Novinar> Novinar { get; set; }
    }
}
