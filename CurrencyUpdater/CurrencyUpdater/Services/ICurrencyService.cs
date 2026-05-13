namespace CurrencyUpdater.Services
{
    internal interface ICurrencyService
    {
        Task UpdateCurrencies(CancellationToken cancellationToken);
    }
}
