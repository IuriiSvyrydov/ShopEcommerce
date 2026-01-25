

using Currency.Domain;
using Currency.Domain.Entities;
using Currency.Domain.ValueObjects;

namespace Currency.Application.Interfaces;

public interface ICurrencyService
{
    Task<Money> ConvertAsync(Money money, 
        Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default);
    Task<CurrencyRate>GetRateAsync(Domain.ValueObjects.Currency baseCurrency, 
        Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default);
    Task<CurrencyRate>RefreshRateAsync(Domain.ValueObjects.Currency baseCurrency, 
        Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default);
}
