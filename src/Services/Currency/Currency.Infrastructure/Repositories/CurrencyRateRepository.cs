namespace Currency.Infrastructure.Repositories;

internal class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly IMongoCollection<CurrencyRateDocument> _collection;
    public CurrencyRateRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<CurrencyRateDocument>("CurrencyRates");
    }
    public async Task<CurrencyRate?> GetAsync(Domain.ValueObjects.Currency baseCurrency, Domain.ValueObjects.Currency targetCurrency, CancellationToken ct = default)
    {
        var doc = await _collection.Find(x=>
                x.BaseCurrency==baseCurrency.Code &&
                x.TargetCurrency == targetCurrency.Code)
            .FirstOrDefaultAsync(ct);
        return doc is null ? null : CurrencyRate.Restore(
            doc.Id,
            Domain.ValueObjects.Currency.From(doc.BaseCurrency),
            Domain.ValueObjects.Currency.From(doc.TargetCurrency),
            doc.Rate,
            new DateTimeOffset(doc.ValidUntil,TimeSpan.Zero));

    }
    // Save or update currency rate
    public async Task SaveAsync(CurrencyRate rate, CancellationToken ct = default)
    {
        var doc = new CurrencyRateDocument
        {
            Id = rate.Id,
            BaseCurrency = rate.BaseCurrency.Code,
            TargetCurrency = rate.TargetCurrency.Code,
            Rate = rate.Rate,
            ValidUntil = rate.ValidUntilUtc.UtcDateTime
        };
        var filter = Builders<CurrencyRateDocument>.Filter.Where(x=>
        x.BaseCurrency==rate.BaseCurrency.Code &&
        x.TargetCurrency==rate.TargetCurrency.Code);
        await _collection.ReplaceOneAsync(filter, doc, new ReplaceOptions { IsUpsert = true }, ct);
    }
}
