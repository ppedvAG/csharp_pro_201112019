using Microsoft.EntityFrameworkCore;
using ppedv.FixDox.Model;
using System;

namespace ppedv.FixDox.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Content> Contents { get; set; }
        public DbSet<Medium> Medien { get; set; }
        public DbSet<Device> Devices { get; set; }

        string conString;
        public EfContext(string conString)
        {
            this.conString = conString;
        }

        public EfContext() : this("Server=.\\SQLEXPRESS;Database=FixDox_Test;Trusted_Connection=true")
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
