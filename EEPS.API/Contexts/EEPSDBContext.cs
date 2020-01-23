using EEPS.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEPS.API.Contexts
{
    public class EEPSDBContext : DbContext
    {
        public EEPSDBContext() { }
        public EEPSDBContext(DbContextOptions<EEPSDBContext> options)
          : base(options)
        {
        }

        public DbSet<CustomerDetail> CustomerDetails { get; set; }
        public DbSet<EventDetail> EventDetails { get; set; }
        public DbSet<GuestDetail> GuestDetails { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = .\\SQLExpress; Database = EEPSDB; Trusted_Connection = True;");
        }

    }
}
