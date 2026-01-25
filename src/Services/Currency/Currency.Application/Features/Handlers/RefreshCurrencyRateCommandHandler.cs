using Currency.Application.Features.Commands;
using MediatR;

namespace Currency.Application.Features.Handlers;

public class RefreshCurrencyRateCommandHandler : IRequestHandler<RefreshCurrencyRateCommand,CurrencyRateDto>
{
    private readonly ICurrencyService _currencyService;

    public RefreshCurrencyRateCommandHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task<CurrencyRateDto> Handle(RefreshCurrencyRateCommand request, CancellationToken cancellationToken)
    {
        var rate = await _currencyService.RefreshRateAsync(Domain.ValueObjects.Currency.From(request.BaseCurrency),
            Domain.ValueObjects.Currency.From(request.TargetCurrency));
        return new CurrencyRateDto(rate.Rate, rate.BaseCurrency.Code, rate.TargetCurrency.Code, rate.UpdatedAtUtc);


    }
}