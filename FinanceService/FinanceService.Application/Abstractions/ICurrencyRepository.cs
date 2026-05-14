using FinanceService.Domain.Entities;

namespace FinanceService.Application.Abstractions
{
    public interface ICurrencyRepository
    {
        Task<Currency?> GetById(string id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Currency>> GetByIds(IEnumerable<string> ids, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Currency>> GetAll(CancellationToken cancellationToken);
    }
}
