using FinanceService.Application.Abstractions;
using FinanceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Infrastructure.Persistence
{
    public class CurrencyRepository(FinanceDbContext context) : ICurrencyRepository
    {
        public async Task<Currency?> GetById(string id, CancellationToken cancellationToken)
        {
            return await context.Currencies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Currency>> GetByIds(IEnumerable<string> ids, CancellationToken cancellationToken)
        {
            return await context.Currencies.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Currency>> GetAll(CancellationToken cancellationToken)
        {
            return await context.Currencies.ToListAsync(cancellationToken);
        }
    }
}
