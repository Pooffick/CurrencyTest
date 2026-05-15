using System.Reflection;
using FinanceService.Application.Abstractions;
using FinanceService.Application.Users.Commands;
using FinanceService.Application.Users.Commands.Handlers;
using FinanceService.Domain.Entities;
using Moq;

namespace FinanceService.Tests
{
    public class AddFavoriteCurrencyCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_AddFavorite_WhenNotAlreadyExists()
        {
            var userId = Guid.NewGuid().ToString();
            var currencyId = Guid.NewGuid().ToString();

            var usd = new Currency("USD", 100m);

            var idProp = typeof(Currency).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

            idProp.SetValue(usd, currencyId);

            var currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock
                .Setup(r => r.GetById(currencyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(usd);

            var favoriteRepoMock = new Mock<IFavoriteCurrencyRepository>();
            favoriteRepoMock
                .Setup(r => r.Exists(userId, currencyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var handler = new AddFavoriteCurrencyCommandHandler(currencyRepoMock.Object, favoriteRepoMock.Object);

            await handler.Handle(new AddFavoriteCurrencyCommand(userId, currencyId), CancellationToken.None);

            favoriteRepoMock.Verify(r => r.Add(It.IsAny<FavoriteCurrency>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Not_Add_WhenAlreadyExists()
        {
            var userId = Guid.NewGuid().ToString();
            var currencyId = Guid.NewGuid().ToString();

            var usd = new Currency("USD", 100m);

            var idProp = typeof(Currency).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

            idProp.SetValue(usd, currencyId);

            var currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock
                .Setup(r => r.GetById(currencyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(usd);

            var favoriteRepoMock = new Mock<IFavoriteCurrencyRepository>();
            favoriteRepoMock
                .Setup(r => r.Exists(userId, currencyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new AddFavoriteCurrencyCommandHandler(currencyRepoMock.Object, favoriteRepoMock.Object);

            await handler.Handle(new AddFavoriteCurrencyCommand(userId, currencyId), CancellationToken.None);

            favoriteRepoMock.Verify(r => r.Add(It.IsAny<FavoriteCurrency>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Throw_WhenCurrencyNotFound()
        {
            var userId = Guid.NewGuid().ToString();
            var currencyId = Guid.NewGuid().ToString();

            var currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock
                .Setup(r => r.GetById(currencyId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Currency)null!);

            var handler = new AddFavoriteCurrencyCommandHandler(currencyRepoMock.Object, Mock.Of<IFavoriteCurrencyRepository>());

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(new AddFavoriteCurrencyCommand(userId, currencyId), CancellationToken.None));
        }
    }
}
