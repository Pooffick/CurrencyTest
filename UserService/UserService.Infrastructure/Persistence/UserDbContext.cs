using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence
{
    public class UserDbContext(DbContextOptions<UserDbContext> opts) : DbContext(opts)
    {
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<User>(e =>
            {
                e.ToTable("user");
                e.HasKey(u => u.Id);
                e.Property(u => u.Id).HasColumnName("id");
                e.Property(u => u.Name)
                    .HasColumnName("name")
                    .IsRequired();
                e.Property(u => u.PasswordHash)
                    .HasColumnName("password_hash")
                    .IsRequired();
            });
        }
    }
}
