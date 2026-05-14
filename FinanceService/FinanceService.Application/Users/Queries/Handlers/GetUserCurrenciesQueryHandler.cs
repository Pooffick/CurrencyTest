using FinanceService.Application.Abstractions;
using FinanceService.Application.Users.Dtos;
using MediatR;

namespace FinanceService.Application.Users.Queries.Handlers
{
    public class GetUserCurrenciesQueryHandler(ICurrencyRepository currencyRepository, IFavoriteCurrencyRepository favoriteRepository)
        : IRequestHandler<GetUserCurrenciesQuery, IReadOnlyCollection<CurrencyDto>>
    {

        public async Task<IReadOnlyCollection<CurrencyDto>> Handle(GetUserCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var favorites = await favoriteRepository.GetByUserId(request.UserId, cancellationToken);
            var currencyIds = favorites.Select(x => x.CurrencyId).ToList();

            var currencies = await currencyRepository.GetByIds(currencyIds, cancellationToken);

            return [.. currencies.Select(c => new CurrencyDto(c.Id, c.Name, c.Rate))];
        }
    }
}
