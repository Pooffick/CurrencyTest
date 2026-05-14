using UserService.Domain.Entities;

namespace UserService.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByName(string name, CancellationToken ct);
        Task<User?> GetById(string id, CancellationToken ct);
        Task Add(User user, CancellationToken ct);
    }
}
