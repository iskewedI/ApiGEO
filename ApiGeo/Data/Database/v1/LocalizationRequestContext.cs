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
            var localizations = new[]
            {
                new Localization
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a")
                }
            };

            LocalizationRequest.AddRange(localizations);
            SaveChanges();
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
