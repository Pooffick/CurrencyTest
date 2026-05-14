using MediatR;
using UserService.Application.Abstractions;
using UserService.Application.Users.Dtos;
using UserService.Domain.Entities;

namespace UserService.Application.Users.Commands.Handlers
{
    public class RegisterUserCommandHandler(IUserRepository users, IPasswordHasher hasher, IJwtTokenGenerator tokens) : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        private readonly IUserRepository _users = users;
        private readonly IPasswordHasher _hasher = hasher;
        private readonly IJwtTokenGenerator _tokens = tokens;

        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var existing = await _users.GetByName(request.Name, ct);
            if (existing is not null)
                throw new InvalidOperationException("User already exists");

            var hash = _hasher.Hash(request.Password);
            var user = new User(request.Name, hash);
            await _users.Add(user, ct);

            var token = _tokens.GenerateToken(user);
            return new AuthResponse(user.Id, user.Name, token);
        }
    }
}
