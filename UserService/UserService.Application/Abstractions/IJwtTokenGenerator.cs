using UserService.Domain.Entities;

namespace UserService.Application.Abstractions
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
