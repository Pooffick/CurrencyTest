namespace FinanceService.Domain.Entities
{
    public class FavoriteCurrency
    {
        public string Id { get; private set; } = null!;
        public string UserId { get; private set; } = null!;
        public string CurrencyId { get; private set; } = null!;

        private FavoriteCurrency() { }

        public FavoriteCurrency(string userId, string currencyId)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            CurrencyId = currencyId;
        }
    }
}
