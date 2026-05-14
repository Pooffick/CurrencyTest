using MediatR;

namespace FinanceService.Application.Users.Commands
{
    public record AddFavoriteCurrencyCommand(string UserId, string CurrencyId) : IRequest;
}
