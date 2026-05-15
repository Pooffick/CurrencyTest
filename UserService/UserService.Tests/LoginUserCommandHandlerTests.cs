using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UserService.Application.Abstractions;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Commands.Handlers;
using UserService.Application.Users.Dtos;
using UserService.Domain.Entities;

namespace UserService.Tests
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Token_WhenCredentialsAreValid()
        {
            var user = new User("Alice", "hashed");
            var command = new LoginUserCommand("Alice", "password");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.GetByName("Alice", It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var hasherMock = new Mock<IPasswordHasher>();
            hasherMock
                .Setup(h => h.Verify("password", "hashed"))
                .Returns(true);

            var jwtMock = new Mock<IJwtTokenGenerator>();
            jwtMock
                .Setup(j => j.GenerateToken(user))
                .Returns("jwt-token");

            var handler = new LoginUserCommandHandler(
                userRepoMock.Object,
                hasherMock.Object,
                jwtMock.Object);

            AuthResponse response = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Alice", response.Name);
            Assert.Equal("jwt-token", response.Token);
        }

        [Fact]
        public async Task Handle_Should_Throw_WhenCredentialsAreInvalid()
        {
            var user = new User("Alice", "hashed");
            var command = new LoginUserCommand("Alice", "wrong");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.GetByName("Alice", It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var hasherMock = new Mock<IPasswordHasher>();
            hasherMock
                .Setup(h => h.Verify("wrong", "hashed"))
                .Returns(false);

            var handler = new LoginUserCommandHandler(
                userRepoMock.Object,
                hasherMock.Object,
                Mock.Of<IJwtTokenGenerator>());

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
