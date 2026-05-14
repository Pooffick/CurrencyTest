using MediatR;

namespace UserService.Application.Users.Commands
{
    public record LogoutUserCommand : IRequest;
}
