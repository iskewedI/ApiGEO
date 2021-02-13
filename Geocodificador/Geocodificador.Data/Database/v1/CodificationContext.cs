using Geocodificador.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geocodificador.Data.Database.v1
{
    public class CodificationContext : DbContext
    {
        public CodificationContext() { }

        public CodificationContext(DbContextOptions<CodificationContext> options) : base(options)
        {
            var codifications = new[]
            {
                new Codification
                {
                    Display_Name = "s",
                }
            };

            Codification.AddRange(codifications);
            SaveChanges();
        }

        public virtual DbSet<Codification> Codification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Codification>(entity =>
            {
                //entity.Property(e => e.X).HasDefaultValueSql("(newid())");
            });
        }
    }
}

