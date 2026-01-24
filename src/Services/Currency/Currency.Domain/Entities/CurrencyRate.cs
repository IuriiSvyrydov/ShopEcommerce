using Currency.Domain.Exceptions;

namespace Currency.Domain.Entities;

public sealed class CurrencyRate
{
    public Guid Id { get; private set; }
    public ValueObjects.Currency BaseCurrency { get; }
    public ValueObjects.Currency TargetCurrency { get; }
    public decimal Rate { get; }
    public DateTimeOffset ValidUntilUtc { get; }

    public bool IsExpired => DateTimeOffset.UtcNow >= ValidUntilUtc;

    public CurrencyRate(
        ValueObjects.Currency baseCurrency,
        ValueObjects.Currency targetCurrency,
        decimal rate,
        DateTimeOffset validUntilUtc)
    {
        if (rate <= 0)
            throw new DomainException(new[]
            {
                new CurrencyErrorItem("InvalidRate", "Rate must be positive.")
            });

        Id = Guid.NewGuid();
        BaseCurrency = baseCurrency;
        TargetCurrency = targetCurrency;
        Rate = rate;
        ValidUntilUtc = validUntilUtc;
    }

    // Создание нового курса
    public static CurrencyRate Create(
        ValueObjects.Currency baseCurrency,
        ValueObjects.Currency targetCurrency,
        decimal rate,
        TimeSpan ttl)
    {
        return new CurrencyRate(
            baseCurrency,
            targetCurrency,
            rate,
            DateTimeOffset.UtcNow.Add(ttl));
    }

    // Восстановление из БД
    public static CurrencyRate Restore(
        Guid id,
        ValueObjects.Currency baseCurrency,
        ValueObjects.Currency targetCurrency,
        decimal rate,
        DateTimeOffset validUntilUtc)
    {
        return new CurrencyRate(
            baseCurrency,
            targetCurrency,
            rate,
            validUntilUtc)
        {
            Id = id
        };
    }
}
