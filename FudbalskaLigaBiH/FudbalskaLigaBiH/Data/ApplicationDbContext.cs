using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FudbalskaLigaBiH.EntityModels;

namespace FudbalskaLigaBiH.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trener> Trener { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server = app.fit.ba,1431;
                                          Database = p2036;
                                          Trusted_Connection = False;
                                          User ID = p2036;
                                          Password = n?7i97Ek;
                                          MultipleActiveResultSets = True;");
        }
    }
}
