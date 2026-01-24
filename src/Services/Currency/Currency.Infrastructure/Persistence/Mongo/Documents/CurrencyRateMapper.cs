internal static class CurrencyRateMapper
{
    public static CurrencyRateDocument ToDocument(CurrencyRate entity)
        => new CurrencyRateDocument
        {
            Id = entity.Id,
            BaseCurrency = entity.BaseCurrency.Code,
            TargetCurrency = entity.TargetCurrency.Code,
            Rate = entity.Rate,
            ValidUntil = entity.ValidUntilUtc.UtcDateTime
        };


    public static CurrencyRate ToEntity(CurrencyRateDocument doc)
          => CurrencyRate.Restore(
              doc.Id,
              Currency.Domain.ValueObjects.Currency.From(doc.BaseCurrency),
              Currency.Domain.ValueObjects.Currency.From(doc.TargetCurrency),
              doc.Rate,
              DateTime.SpecifyKind(doc.ValidUntil, DateTimeKind.Utc)
          );
} 