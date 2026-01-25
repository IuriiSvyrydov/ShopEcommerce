using MediatR;

namespace Currency.Application.Features.Commands;

public record RefreshCurrencyRateCommand(string BaseCurrency, string TargetCurrency)
    :IRequest<CurrencyRateDto>;
public    record CurrencyRateDto(decimal Rate, string BaseCurrency, string TargetCurrency,DateTimeOffset UpdateAtUtc);