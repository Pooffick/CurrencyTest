namespace FinanceService.Domain.Entities
{
    public class Currency
    {
        public string Id { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public decimal Rate { get; set; }

        private Currency() { }

        public Currency(string name, decimal rate)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Rate = rate;
        }
    }
}
