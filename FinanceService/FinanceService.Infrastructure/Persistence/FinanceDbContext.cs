using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Persistence
{
    public class FinanceDbContext(DbContextOptions<FinanceDbContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<FavoriteCurrency> FavoriteCurrencies => Set<FavoriteCurrency>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name").IsRequired();
                entity.Property(e => e.Rate).HasColumnName("rate").IsRequired();
            });

            modelBuilder.Entity<FavoriteCurrency>(entity =>
            {
                entity.ToTable("favorites");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
                entity.Property(e => e.CurrencyId).HasColumnName("currency_id").IsRequired();
            });
        }
    }
}
