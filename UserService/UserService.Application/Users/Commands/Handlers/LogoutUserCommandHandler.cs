using MediatR;

namespace UserService.Application.Users.Commands.Handlers
{
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
    {
        // I don't understand why the logout command is needed when using jwt.
        // Maybe it should be added to blacklist, but I'll leave that for discussion.
        public Task Handle(LogoutUserCommand request, CancellationToken ct) => Task.CompletedTask;
    }
}
