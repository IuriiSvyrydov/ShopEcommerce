using Currency.Domain.Interfaces;
using System.Net.Http.Json;

namespace Currency.Infrastructure.ExternalApis;

public class ExchangeRateApiProvider : ICurrencyRateProvider
{
    private readonly HttpClient _httpClient;
    public ExchangeRateApiProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<decimal> GetRateAsync(
     Domain.ValueObjects.Currency baseCurrency,
     Domain.ValueObjects.Currency targetCurrency,
     CancellationToken ct = default)
    {
        if (baseCurrency.Equals(targetCurrency))
            return 1m;

        // Всегда берём курс из UAH
        var url = $"https://open.er-api.com/v6/latest/UAH";
        var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponse>(url, ct);

        if (response is null ||
            !response.Rates.TryGetValue(targetCurrency.Code, out var uahToTarget))
            throw new InvalidOperationException("Failed to fetch currency rate");


        return 1m / uahToTarget;
    }

    private class ExchangeRateResponse
    {
     
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}

