using System.Text;
using CurrencyTest.Database;
using CurrencyUpdater.Services;
using CurrencyUpdater.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient();

            builder.Services.AddTransient<ICurrencyService, CbrCurrencyService>();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using var context = services.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
            }

            host.Run();
        }
    }
}