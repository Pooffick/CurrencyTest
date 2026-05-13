using System.Globalization;
using System.Xml;
using CurrencyTest.Database;
using CurrencyTest.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyUpdater.Services.Implementation
{
    public class CbrCurrencyService(HttpClient httpClient, DatabaseContext dbContext) : ICurrencyService
    {
        private const string CbrUrl = "http://www.cbr.ru/scripts/XML_daily.asp";
        private static readonly CultureInfo RussianCulture = new("ru-RU");

        public async Task UpdateCurrencies(CancellationToken cancellationToken)
        {
            var currencies = await GetCurrenciesFromCbr(cancellationToken);

            foreach (var currency in currencies)
            {
                var existing = await dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == currency.Id, cancellationToken);
                if (existing == null)
                {
                    dbContext.Currencies.Add(currency);
                }
                else
                {
                    existing.Rate = currency.Rate;
                    dbContext.Currencies.Update(existing);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<List<Currency>> GetCurrenciesFromCbr(CancellationToken ct)
        {
            var response = await httpClient.GetAsync(CbrUrl, ct);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync(ct);
            var document = new XmlDocument();
            document.Load(stream);

            var result = new List<Currency>();

            var valuteNodes = document.GetElementsByTagName("Valute");
            foreach (XmlNode node in valuteNodes)
            {
                var id = node.Attributes?["ID"]?.Value;
                var name = node["Name"]?.InnerText;
                var rateString = node["VunitRate"]?.InnerText;

                if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(rateString))
                    continue;

                // Parse the rate using Russian culture because the XML uses
                // commas as decimal separators (e.g. "87,3791")
                if (!decimal.TryParse(rateString, NumberStyles.Any, RussianCulture, out var rate))
                    continue;

                var currency = new Currency
                {
                    Id = id,
                    Name = name,
                    Rate = rate
                };
                result.Add(currency);
            }

            return result;
        }
    }
}
