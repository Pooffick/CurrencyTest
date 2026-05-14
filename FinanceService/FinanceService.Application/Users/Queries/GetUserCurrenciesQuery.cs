using FinanceService.Application.Users.Dtos;
using MediatR;

namespace FinanceService.Application.Users.Queries
{
    public record GetUserCurrenciesQuery(string UserId) : IRequest<IReadOnlyCollection<CurrencyDto>>;
}
