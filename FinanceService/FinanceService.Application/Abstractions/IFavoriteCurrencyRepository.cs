using FinanceService.Domain.Entities;

namespace FinanceService.Application.Abstractions
{
    public interface IFavoriteCurrencyRepository
    {
        Task<IReadOnlyCollection<FavoriteCurrency>> GetByUserId(string userId, CancellationToken cancellationToken);
        Task Add(FavoriteCurrency favorite, CancellationToken cancellationToken);
        Task<bool> Exists(string userId, string currencyId, CancellationToken cancellationToken);
        Task Remove(FavoriteCurrency favorite, CancellationToken cancellationToken);
    }
}
