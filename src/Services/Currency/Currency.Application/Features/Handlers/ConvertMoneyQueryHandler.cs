using Currency.Application.Features.Queries;
using Currency.Domain.ValueObjects;
using MediatR;

namespace Currency.Application.Features.Handlers;

public sealed class ConvertMoneyQueryHandler : IRequestHandler<ConvertMoneyQuery, ConvertedMoneyDto>
{
    private readonly ICurrencyService _currencyService;
    public ConvertMoneyQueryHandler(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    public async Task<ConvertedMoneyDto> Handle(ConvertMoneyQuery request, CancellationToken cancellationToken)
    {
        var money = new Money(request.Amount, Domain.ValueObjects.Currency.From(request.FromCurrency));
        var converted = await _currencyService.ConvertAsync(money, Domain.ValueObjects.Currency.From(request.ToCurrency));
        return new ConvertedMoneyDto(converted.Amount, converted.Currency.Code);

    }
}
