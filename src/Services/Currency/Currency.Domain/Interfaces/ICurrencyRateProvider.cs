namespace Currency.Domain.Interfaces;

public interface ICurrencyRateProvider
{
    Task<decimal> GetRateAsync(ValueObjects.Currency baseCurrency,
       ValueObjects.Currency targetCurrency,
        CancellationToken ct = default);
}