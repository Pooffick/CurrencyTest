using Microsoft.EntityFrameworkCore;
using UserService.Application.Abstractions;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence
{
    public class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task Add(User user, CancellationToken ct)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(ct);
        }

        public async Task<User?> GetById(string id, CancellationToken ct) =>
            await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task<User?> GetByName(string name, CancellationToken ct) =>
            await dbContext.Users.FirstOrDefaultAsync(u => u.Name == name, ct);
    }
}
