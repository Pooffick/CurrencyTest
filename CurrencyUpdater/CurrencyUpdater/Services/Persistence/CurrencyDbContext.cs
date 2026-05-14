using CurrencyUpdater.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater.Services.Persistence
{
    public class CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .ValueGeneratedNever();

                entity.Property(e => e.Name)
                      .HasColumnName("name")
                      .IsRequired();

                entity.Property(e => e.Rate)
                      .HasColumnName("rate")
                      .IsRequired();
            });
        }
    }
}
