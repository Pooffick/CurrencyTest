using System.Reflection;
using FinanceService.Application.Abstractions;
using FinanceService.Application.Users.Dtos;
using FinanceService.Application.Users.Queries;
using FinanceService.Application.Users.Queries.Handlers;
using FinanceService.Domain.Entities;
using Moq;

namespace FinanceService.Tests
{
    public class GetUserCurrenciesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Favorites_ForUser()
        {
            var userId = Guid.NewGuid().ToString();

            var favoriteRepoMock = new Mock<IFavoriteCurrencyRepository>();
            favoriteRepoMock
                .Setup(r => r.GetByUserId(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                [
                new FavoriteCurrency(userId, "R11111"),
                new FavoriteCurrency(userId, "R22222")
                ]);

            var usd = new Currency("USD", 100m);
            var eur = new Currency("EUR", 200m);

            var idProp = typeof(Currency).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

            idProp.SetValue(usd, "R11111");
            idProp.SetValue(eur, "R22222");

            var currencyRepoMock = new Mock<ICurrencyRepository>();
            currencyRepoMock
                .Setup(r => r.GetByIds(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync([ usd, eur ]);

            var handler = new GetUserCurrenciesQueryHandler(currencyRepoMock.Object, favoriteRepoMock.Object);

            IReadOnlyCollection<CurrencyDto> result = await handler.Handle(new GetUserCurrenciesQuery(userId), CancellationToken.None);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "USD" && c.Rate == 100m);
            Assert.Contains(result, c => c.Name == "EUR" && c.Rate == 200m);
        }
    }
}