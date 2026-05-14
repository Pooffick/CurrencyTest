using MediatR;
using UserService.Application.Users.Dtos;

namespace UserService.Application.Users.Commands
{
    public record LoginUserCommand(string Name, string Password) : IRequest<AuthResponse>;
}
