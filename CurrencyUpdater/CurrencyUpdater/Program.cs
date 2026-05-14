using System.Text;
using CurrencyUpdater.Services;
using CurrencyUpdater.Services.Implementation;
using CurrencyUpdater.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<CurrencyDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient();

            builder.Services.AddTransient<ICurrencyService, CbrCurrencyService>();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();

            host.Run();
        }
    }
}