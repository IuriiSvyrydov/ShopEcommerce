
using Currency.Domain;
using Currency.Domain.Entities;

namespace Currency.Application.Interfaces;

public interface ICurrencyRateRepository
{
    /// <summary>
    /// Возвращает актуальный курс, если он есть и не протух
    /// </summary>
    Task<CurrencyRate?> GetAsync(
        Domain.ValueObjects.Currency baseCurrency,
        Domain.ValueObjects.Currency targetCurrency,
        CancellationToken ct = default);

    /// <summary>
    /// Сохраняет или обновляет курс
    /// </summary>
    Task SaveAsync(
        CurrencyRate rate,
        CancellationToken ct = default);
}

