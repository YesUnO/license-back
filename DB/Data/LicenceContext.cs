using license_back.DB.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license_back.DB.Data
{
    public class LicenceContext:DbContext
    {
        public LicenceContext(DbContextOptions<LicenceContext> options):base(options)
        {
        }

        public DbSet<Licence> License { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Licence>().ToTable("Licenses");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
