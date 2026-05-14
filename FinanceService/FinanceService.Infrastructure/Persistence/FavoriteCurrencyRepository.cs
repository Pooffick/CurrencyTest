using FinanceService.Application.Abstractions;
using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Persistence
{
    public class FavoriteCurrencyRepository(FinanceDbContext context) : IFavoriteCurrencyRepository
    {
        public async Task<IReadOnlyCollection<FavoriteCurrency>> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            return await context.FavoriteCurrencies
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task Add(FavoriteCurrency favorite, CancellationToken cancellationToken)
        {
            context.FavoriteCurrencies.Add(favorite);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove(FavoriteCurrency favorite, CancellationToken cancellationToken)
        {
            context.FavoriteCurrencies.Remove(favorite);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(string userId, string currencyId, CancellationToken cancellationToken)
        {
            return await context.FavoriteCurrencies
                .AnyAsync(x => x.UserId == userId && x.CurrencyId == currencyId, cancellationToken);
        }
    }
}
