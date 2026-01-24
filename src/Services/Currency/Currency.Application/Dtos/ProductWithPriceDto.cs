

namespace Currency.Application.Dtos;

public record ProductWithPriceDto(Guid Id,
    string Name, decimal Price, string CurrencyCode);

