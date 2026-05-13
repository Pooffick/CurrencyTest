using CurrencyUpdater.Services;

namespace CurrencyUpdater
{
    public class Worker : BackgroundService
    {
        private const int DefaultUpdateIntervalMinutes = 1;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _updateInterval;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            var minutes = configuration.GetValue<int?>("UpdateIntervalMinutes");
            _updateInterval = TimeSpan.FromMinutes(minutes ?? DefaultUpdateIntervalMinutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Currency updater started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var currencyService = scope.ServiceProvider.GetRequiredService<ICurrencyService>();

                    await currencyService.UpdateCurrencies(stoppingToken);

                    _logger.LogInformation("Currencies updated successfully at {Time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating currencies");
                }

                // Wait until next iteration or cancellation
                try
                {
                    await Task.Delay(_updateInterval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }
            }
        }
    }
}
