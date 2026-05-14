using FinanceService.Application.Abstractions;
using FinanceService.Domain.Entities;
using MediatR;

namespace FinanceService.Application.Users.Commands.Handlers
{
    public class AddFavoriteCurrencyCommandHandler(ICurrencyRepository currencyRepository, IFavoriteCurrencyRepository favoriteCurrencyRepository) : IRequestHandler<AddFavoriteCurrencyCommand>
    {
        public async Task Handle(AddFavoriteCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await currencyRepository.GetById(request.CurrencyId, cancellationToken);
            if (currency == null)
                throw new InvalidOperationException("Currency not found");

            var alreadyExists = await favoriteCurrencyRepository.Exists(request.UserId, request.CurrencyId, cancellationToken);
            if (alreadyExists)
                return;

            var favoriteCurrency = new FavoriteCurrency(request.UserId, request.CurrencyId);
            await favoriteCurrencyRepository.Add(favoriteCurrency, cancellationToken);
        }
    }
}
