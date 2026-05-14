using MediatR;
using UserService.Application.Abstractions;
using UserService.Application.Users.Dtos;

namespace UserService.Application.Users.Commands.Handlers
{
    public class LoginUserCommandHandler(IUserRepository users, IPasswordHasher hasher, IJwtTokenGenerator tokens) : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        private readonly IUserRepository _users = users;
        private readonly IPasswordHasher _hasher = hasher;
        private readonly IJwtTokenGenerator _tokens = tokens;

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken ct)
        {
            var user = await _users.GetByName(request.Name, ct)
                       ?? throw new UnauthorizedAccessException("Invalid credentials");

            if (!_hasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _tokens.GenerateToken(user);
            return new AuthResponse(user.Id, user.Name, token);
        }
    }

}
