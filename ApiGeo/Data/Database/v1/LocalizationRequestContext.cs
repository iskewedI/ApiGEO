using System;
using GeoApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Data.Database.v1
{
    public class LocalizationRequestContext : DbContext
    {
        public LocalizationRequestContext()
        {
        }

        public LocalizationRequestContext(DbContextOptions<LocalizationRequestContext> options)
            : base(options)
        {
        }

        public DbSet<Localization> LocalizationRequest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Localization>(entity =>
            {
                //entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                //entity.Property(e => e.Birthday).HasColumnType("date");

                //entity.Property(e => e.FirstName).IsRequired();

                //entity.Property(e => e.LastName).IsRequired();
            });
        }
    }
}
