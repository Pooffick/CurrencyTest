using Moq;
using UserService.Application.Abstractions;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Commands.Handlers;
using UserService.Application.Users.Dtos;
using UserService.Domain.Entities;

namespace UserService.Tests
{
    public class RegisterUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_AddUser_And_Return_AuthResponse()
        {
            var command = new RegisterUserCommand("Alice", "password");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.GetByName(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null!);
            userRepoMock
                .Setup(r => r.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var passwordHasherMock = new Mock<IPasswordHasher>();
            passwordHasherMock
                .Setup(h => h.Hash("password"))
                .Returns("hashed-password");

            var jwtMock = new Mock<IJwtTokenGenerator>();
            jwtMock
                .Setup(j => j.GenerateToken(It.IsAny<User>()))
                .Returns("jwt-token");

            var handler = new RegisterUserCommandHandler(
                userRepoMock.Object,
                passwordHasherMock.Object,
                jwtMock.Object);

            AuthResponse response = await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Alice", response.Name);
            Assert.Equal("jwt-token", response.Token);
            userRepoMock.Verify(r => r.Add(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_WhenUserAlreadyExists()
        {
            var existing = new User("Bob", "hash");
            var command = new RegisterUserCommand("Bob", "password");

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.GetByName("Bob", It.IsAny<CancellationToken>()))
                .ReturnsAsync(existing);

            var handler = new RegisterUserCommandHandler(userRepoMock.Object, Mock.Of<IPasswordHasher>(), Mock.Of<IJwtTokenGenerator>());

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}