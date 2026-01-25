using MediatR;

namespace Currency.Application.Features.Queries;
public record ConvertMoneyQuery(decimal Amount,
    string FromCurrency,
    string ToCurrency
):IRequest<ConvertedMoneyDto>;
public record ConvertedMoneyDto(decimal Amount, string CurrencyCode);
